using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace PhotoViewer.App.Views
{
    /// <summary>
    /// Логика взаимодействия для PhotoPage.xaml
    /// </summary>
    public partial class PhotoPage : System.Windows.Controls.Page
    {
        public PhotoPage() {
            InitializeComponent();
        }
    }










    public class ActivityFeedLayout : VirtualizingLayout // STEP #1 Inherit from base attached layout
    {
        // STEP #2 - Parameterize the layout
        #region Layout parameters

        // We'll cache copies of the dependency properties to avoid calling GetValue during layout since that
        // can be quite expensive due to the number of times we'd end up calling these.
        private double _rowSpacing;
        private double _colSpacing;
        private Size _minItemSize = Size.Empty;

        /// <summary>
        /// Gets or sets the size of the whitespace gutter to include between rows
        /// </summary>
        public double RowSpacing {
            get { return _rowSpacing; }
            set { SetValue(RowSpacingProperty, value); }
        }

        /// <summary>
        /// Gets or sets the size of the whitespace gutter to include between items on the same row
        /// </summary>
        public double ColumnSpacing {
            get { return _colSpacing; }
            set { SetValue(ColumnSpacingProperty, value); }
        }

        public Size MinItemSize {
            get { return _minItemSize; }
            set { SetValue(MinItemSizeProperty, value); }
        }

        public static readonly DependencyProperty RowSpacingProperty =
            DependencyProperty.Register(
                nameof(RowSpacing),
                typeof(double),
                typeof(ActivityFeedLayout),
                new PropertyMetadata(0, OnPropertyChanged));

        public static readonly DependencyProperty ColumnSpacingProperty =
            DependencyProperty.Register(
                nameof(ColumnSpacing),
                typeof(double),
                typeof(ActivityFeedLayout),
                new PropertyMetadata(0, OnPropertyChanged));

        public static readonly DependencyProperty MinItemSizeProperty =
            DependencyProperty.Register(
                nameof(MinItemSize),
                typeof(Size),
                typeof(ActivityFeedLayout),
                new PropertyMetadata(Size.Empty, OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args) {
            var layout = obj as ActivityFeedLayout;
            if (args.Property == RowSpacingProperty) {
                layout._rowSpacing = (double)args.NewValue;
            } else if (args.Property == ColumnSpacingProperty) {
                layout._colSpacing = (double)args.NewValue;
            } else if (args.Property == MinItemSizeProperty) {
                layout._minItemSize = (Size)args.NewValue;
            } else {
                throw new InvalidOperationException("Don't know what you are talking about!");
            }

            layout.InvalidateMeasure();
        }

        #endregion

        #region Setup / teardown // STEP #3: Initialize state

        protected override void InitializeForContextCore(VirtualizingLayoutContext context) {
            base.InitializeForContextCore(context);

            var state = context.LayoutState as ActivityFeedLayoutState;
            if (state == null) {
                // Store any state we might need since (in theory) the layout could be in use by multiple
                // elements simultaneously
                // In reality for the Xbox Activity Feed there's probably only a single instance.
                context.LayoutState = new ActivityFeedLayoutState();
            }
        }

        protected override void UninitializeForContextCore(VirtualizingLayoutContext context) {
            base.UninitializeForContextCore(context);

            // clear any state
            context.LayoutState = null;
        }

        #endregion

        #region Layout // STEP #4,5 - Measure and Arrange

        protected override Size MeasureOverride(VirtualizingLayoutContext context, Size availableSize) {
            if (this.MinItemSize == Size.Empty) {
                var firstElement = context.GetOrCreateElementAt(0);
                firstElement.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                // setting the member value directly to skip invalidating layout
                this._minItemSize = firstElement.DesiredSize;
            }

            // Determine which rows need to be realized.  We know every row will have the same height and
            // only contain 3 items.  Use that to determine the index for the first and last item that
            // will be within that realization rect.
            var firstRowIndex = Math.Max(
                (int)(context.RealizationRect.Y / (this.MinItemSize.Height + this.RowSpacing)) - 1,
                0);
            var lastRowIndex = Math.Min(
                (int)(context.RealizationRect.Bottom / (this.MinItemSize.Height + this.RowSpacing)) + 1,
                (int)(context.ItemCount / 3));

            // Determine which items will appear on those rows and what the rect will be for each item
            var state = context.LayoutState as ActivityFeedLayoutState;
            state.LayoutRects.Clear();

            // Save the index of the first realized item.  We'll use it as a starting point during arrange.
            state.FirstRealizedIndex = firstRowIndex * 3;

            // ideal item width that will expand/shrink to fill available space
            double desiredItemWidth = Math.Max(this.MinItemSize.Width, (availableSize.Width - this.ColumnSpacing * 3) / 4);

            // Foreach item between the first and last index,
            //     Call GetElementOrCreateElementAt which causes an element to either be realized or retrieved
            //       from a recycle pool
            //     Measure the element using an appropriate size
            //
            // Any element that was previously realized which we don't retrieve in this pass (via a call to
            // GetElementOrCreateAt) will be automatically cleared and set aside for later re-use.
            // Note: While this work fine, it does mean that more elements than are required may be
            // created because it isn't until after our MeasureOverride completes that the unused elements
            // will be recycled and available to use.  We could avoid this by choosing to track the first/last
            // index from the previous layout pass.  The diff between the previous range and current range
            // would represent the elements that we can pre-emptively make available for re-use by calling
            // context.RecycleElement(element).
            for (int rowIndex = firstRowIndex; rowIndex < lastRowIndex; rowIndex++) {
                int firstItemIndex = rowIndex * 3;
                var boundsForCurrentRow = CalculateLayoutBoundsForRow(rowIndex, desiredItemWidth);

                for (int columnIndex = 0; columnIndex < 3; columnIndex++) {
                    var index = firstItemIndex + columnIndex;
                    var rect = boundsForCurrentRow[index % 3];
                    var container = context.GetOrCreateElementAt(index);

                    container.Measure(
                        new Size(boundsForCurrentRow[columnIndex].Width, boundsForCurrentRow[columnIndex].Height));

                    state.LayoutRects.Add(boundsForCurrentRow[columnIndex]);
                }
            }

            // Calculate and return the size of all the content (realized or not) by figuring out
            // what the bottom/right position of the last item would be.
            var extentHeight = ((int)(context.ItemCount / 3) - 1) * (this.MinItemSize.Height + this.RowSpacing) + this.MinItemSize.Height;

            // Report this as the desired size for the layout
            return new Size(desiredItemWidth * 4 + this.ColumnSpacing * 2, extentHeight);
        }

        protected override Size ArrangeOverride(VirtualizingLayoutContext context, Size finalSize) {
            // walk through the cache of containers and arrange
            var state = context.LayoutState as ActivityFeedLayoutState;
            var virtualContext = context as VirtualizingLayoutContext;
            int currentIndex = state.FirstRealizedIndex;

            foreach (var arrangeRect in state.LayoutRects) {
                var container = virtualContext.GetOrCreateElementAt(currentIndex);
                container.Arrange(arrangeRect);
                currentIndex++;
            }

            return finalSize;
        }

        #endregion
        #region Helper methods

        private Rect[] CalculateLayoutBoundsForRow(int rowIndex, double desiredItemWidth) {
            var boundsForRow = new Rect[3];

            var yoffset = rowIndex * (this.MinItemSize.Height + this.RowSpacing);
            boundsForRow[0].Y = boundsForRow[1].Y = boundsForRow[2].Y = yoffset;
            boundsForRow[0].Height = boundsForRow[1].Height = boundsForRow[2].Height = this.MinItemSize.Height;

            if (rowIndex % 2 == 0) {
                // Left tile (narrow)
                boundsForRow[0].X = 0;
                boundsForRow[0].Width = desiredItemWidth;
                // Middle tile (narrow)
                boundsForRow[1].X = boundsForRow[0].Right + this.ColumnSpacing;
                boundsForRow[1].Width = desiredItemWidth;
                // Right tile (wide)
                boundsForRow[2].X = boundsForRow[1].Right + this.ColumnSpacing;
                boundsForRow[2].Width = desiredItemWidth * 2 + this.ColumnSpacing;
            } else {
                // Left tile (wide)
                boundsForRow[0].X = 0;
                boundsForRow[0].Width = (desiredItemWidth * 2 + this.ColumnSpacing);
                // Middle tile (narrow)
                boundsForRow[1].X = boundsForRow[0].Right + this.ColumnSpacing;
                boundsForRow[1].Width = desiredItemWidth;
                // Right tile (narrow)
                boundsForRow[2].X = boundsForRow[1].Right + this.ColumnSpacing;
                boundsForRow[2].Width = desiredItemWidth;
            }

            return boundsForRow;
        }

        #endregion
    }

    internal class ActivityFeedLayoutState
    {
        public int FirstRealizedIndex { get; set; }

        /// <summary>
        /// List of layout bounds for items starting with the
        /// FirstRealizedIndex.
        /// </summary>
        public List<Rect> LayoutRects {
            get {
                if (_layoutRects == null) {
                    _layoutRects = new List<Rect>();
                }

                return _layoutRects;
            }
        }

        private List<Rect> _layoutRects;
    }

    // This is a sample layout that stacks elements one after
    // the other where each item can be of variable height. This is
    // also a virtualizing layout - we measure and arrange only elements
    // that are in the viewport. Not measuring/arranging all elements means
    // that we do not have the complete picture and need to estimate sometimes.
    // For example the size of the layout (extent) is an estimation based on the
    // average heights we have seen so far. Also, if you drag the mouse thumb
    // and yank it quickly, then we estimate what goes in the new viewport.

    // The layout caches the bounds of everything that are in the current viewport.
    // During measure, we might get a suggested anchor (or start index), we use that
    // index to start and layout the rest of the items in the viewport relative to that
    // index. Note that since we are estimating, we can end up with negative origin when
    // the viewport is somewhere in the middle of the extent. This is achieved by setting the
    // LayoutOrigin property on the context. Once this is set, future viewport will account
    // for the origin.
    public class VirtualizingStackLayout : VirtualizingLayout
    {
        // Estimation state
        List<double> m_estimationBuffer = Enumerable.Repeat(0d, 100).ToList();
        int m_numItemsUsedForEstimation = 0;
        double m_totalHeightForEstimation = 0;

        // State to keep track of realized bounds
        int m_firstRealizedDataIndex = 0;
        List<Rect> m_realizedElementBounds = new List<Rect>();

        Rect m_lastExtent = new Rect();

        protected override Size MeasureOverride(VirtualizingLayoutContext context, Size availableSize) {
            var viewport = context.RealizationRect;
            DebugTrace("MeasureOverride: Viewport " + viewport);

            // Remove bounds for elements that are now outside the viewport.
            // Proactive recycling elements means we can reuse it during this measure pass again.
            RemoveCachedBoundsOutsideViewport(viewport);

            // Find the index of the element to start laying out from - the anchor
            int startIndex = GetStartIndex(context, availableSize);

            // Measure and layout elements starting from the start index, forward and backward.
            Generate(context, availableSize, startIndex, forward: true);
            Generate(context, availableSize, startIndex, forward: false);

            // Estimate the extent size. Note that this can have a non 0 origin.
            m_lastExtent = EstimateExtent(context, availableSize);
            context.LayoutOrigin = new Point(m_lastExtent.X, m_lastExtent.Y);
            return new Size(m_lastExtent.Width, m_lastExtent.Height);
        }

        protected override Size ArrangeOverride(VirtualizingLayoutContext context, Size finalSize) {
            DebugTrace("ArrangeOverride: Viewport" + context.RealizationRect);
            for (int realizationIndex = 0; realizationIndex < m_realizedElementBounds.Count; realizationIndex++) {
                int currentDataIndex = m_firstRealizedDataIndex + realizationIndex;
                DebugTrace("Arranging " + currentDataIndex);

                // Arrange the child. If any alignment needs to be done, it
                // can be done here.
                var child = context.GetOrCreateElementAt(currentDataIndex);
                var arrangeBounds = m_realizedElementBounds[realizationIndex];
                arrangeBounds.X -= m_lastExtent.X;
                arrangeBounds.Y -= m_lastExtent.Y;
                child.Arrange(arrangeBounds);
            }

            return finalSize;
        }

        // The data collection has changed, since we are maintaining the bounds of elements
        // in the viewport, we will update the list to account for the collection change.
        protected override void OnItemsChangedCore(VirtualizingLayoutContext context, object source, NotifyCollectionChangedEventArgs args) {
            InvalidateMeasure();
            if (m_realizedElementBounds.Count > 0) {
                switch (args.Action) {
                    case NotifyCollectionChangedAction.Add:
                        OnItemsAdded(args.NewStartingIndex, args.NewItems.Count);
                        break;
                    case NotifyCollectionChangedAction.Replace:
                        OnItemsRemoved(args.OldStartingIndex, args.OldItems.Count);
                        OnItemsAdded(args.NewStartingIndex, args.NewItems.Count);
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        OnItemsRemoved(args.OldStartingIndex, args.OldItems.Count);
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        m_realizedElementBounds.Clear();
                        m_firstRealizedDataIndex = 0;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        // Figure out which index to use as the anchor and start laying out around it.
        private int GetStartIndex(VirtualizingLayoutContext context, Size availableSize) {
            int startDataIndex = -1;
            var recommendedAnchorIndex = context.RecommendedAnchorIndex;
            bool isSuggestedAnchorValid = recommendedAnchorIndex != -1;

            if (isSuggestedAnchorValid) {
                if (IsRealized(recommendedAnchorIndex)) {
                    startDataIndex = recommendedAnchorIndex;
                } else {
                    ClearRealizedRange();
                    startDataIndex = recommendedAnchorIndex;
                }
            } else {
                // Find the first realized element that is visible in the viewport.
                startDataIndex = GetFirstRealizedDataIndexInViewport(context.RealizationRect);
                if (startDataIndex < 0) {
                    startDataIndex = EstimateIndexForViewport(context.RealizationRect, context.ItemCount);
                    ClearRealizedRange();
                }
            }

            // We have an anchorIndex, realize and measure it and
            // figure out its bounds.
            if (startDataIndex != -1 & context.ItemCount > 0) {
                if (m_realizedElementBounds.Count == 0) {
                    m_firstRealizedDataIndex = startDataIndex;
                }

                var newAnchor = EnsureRealized(startDataIndex);
                DebugTrace("Measuring start index " + startDataIndex);
                var desiredSize = MeasureElement(context, startDataIndex, availableSize);

                var bounds = new Rect(
                    0,
                    newAnchor ?
                        (m_totalHeightForEstimation / m_numItemsUsedForEstimation) * startDataIndex : GetCachedBoundsForDataIndex(startDataIndex).Y,
                    availableSize.Width,
                    desiredSize.Height);
                SetCachedBoundsForDataIndex(startDataIndex, bounds);
            }

            return startDataIndex;
        }


        private void Generate(VirtualizingLayoutContext context, Size availableSize, int anchorDataIndex, bool forward) {
            // Generate forward or backward from anchorIndex until we hit the end of the viewport
            int step = forward ? 1 : -1;
            int previousDataIndex = anchorDataIndex;
            int currentDataIndex = previousDataIndex + step;
            var viewport = context.RealizationRect;
            while (IsDataIndexValid(currentDataIndex, context.ItemCount) &&
                ShouldContinueFillingUpSpace(previousDataIndex, forward, viewport)) {
                EnsureRealized(currentDataIndex);
                DebugTrace("Measuring " + currentDataIndex);
                var desiredSize = MeasureElement(context, currentDataIndex, availableSize);
                var previousBounds = GetCachedBoundsForDataIndex(previousDataIndex);
                Rect currentBounds = new Rect(0,
                                              forward ? previousBounds.Y + previousBounds.Height : previousBounds.Y - desiredSize.Height,
                                              availableSize.Width,
                                              desiredSize.Height);
                SetCachedBoundsForDataIndex(currentDataIndex, currentBounds);
                previousDataIndex = currentDataIndex;
                currentDataIndex += step;
            }
        }

        // Remove bounds that are outside the viewport, leaving one extra since our
        // generate stops after generating one extra to know that we are outside the
        // viewport.
        private void RemoveCachedBoundsOutsideViewport(Rect viewport) {
            int firstRealizedIndexInViewport = 0;
            while (firstRealizedIndexInViewport < m_realizedElementBounds.Count &&
                   !Intersects(m_realizedElementBounds[firstRealizedIndexInViewport], viewport)) {
                firstRealizedIndexInViewport++;
            }

            int lastRealizedIndexInViewport = m_realizedElementBounds.Count - 1;
            while (lastRealizedIndexInViewport >= 0 &&
                !Intersects(m_realizedElementBounds[lastRealizedIndexInViewport], viewport)) {
                lastRealizedIndexInViewport--;
            }

            if (firstRealizedIndexInViewport > 0) {
                m_firstRealizedDataIndex += firstRealizedIndexInViewport;
                m_realizedElementBounds.RemoveRange(0, firstRealizedIndexInViewport);
            }

            if (lastRealizedIndexInViewport >= 0 && lastRealizedIndexInViewport < m_realizedElementBounds.Count - 2) {
                m_realizedElementBounds.RemoveRange(lastRealizedIndexInViewport + 2, m_realizedElementBounds.Count - lastRealizedIndexInViewport - 3);
            }
        }

        private bool Intersects(Rect bounds, Rect viewport) {
            return !(bounds.Bottom < viewport.Top ||
                bounds.Top > viewport.Bottom);
        }

        private bool ShouldContinueFillingUpSpace(int dataIndex, bool forward, Rect viewport) {
            var bounds = GetCachedBoundsForDataIndex(dataIndex);
            return forward ?
                bounds.Y < viewport.Bottom :
                bounds.Y > viewport.Top;
        }

        private bool IsDataIndexValid(int currentDataIndex, int itemCount) {
            return currentDataIndex >= 0 && currentDataIndex < itemCount;
        }

        private int EstimateIndexForViewport(Rect viewport, int dataCount) {
            double averageHeight = m_totalHeightForEstimation / m_numItemsUsedForEstimation;
            int estimatedIndex = (int)(viewport.Top / averageHeight);
            // clamp to an index within the collection
            estimatedIndex = Math.Max(0, Math.Min(estimatedIndex, dataCount));
            return estimatedIndex;
        }

        private int GetFirstRealizedDataIndexInViewport(Rect viewport) {
            int index = -1;
            if (m_realizedElementBounds.Count > 0) {
                for (int i = 0; i < m_realizedElementBounds.Count; i++) {
                    if (m_realizedElementBounds[i].Y < viewport.Bottom &&
                       m_realizedElementBounds[i].Bottom > viewport.Top) {
                        index = m_firstRealizedDataIndex + i;
                        break;
                    }
                }
            }

            return index;
        }

        private Size MeasureElement(VirtualizingLayoutContext context, int index, Size availableSize) {
            var child = context.GetOrCreateElementAt(index);
            child.Measure(availableSize);

            int estimationBufferIndex = index % m_estimationBuffer.Count;
            bool alreadyMeasured = m_estimationBuffer[estimationBufferIndex] != 0;
            if (!alreadyMeasured) {
                m_numItemsUsedForEstimation++;
            }

            m_totalHeightForEstimation -= m_estimationBuffer[estimationBufferIndex];
            m_totalHeightForEstimation += child.DesiredSize.Height;
            m_estimationBuffer[estimationBufferIndex] = child.DesiredSize.Height;

            return child.DesiredSize;
        }

        private bool EnsureRealized(int dataIndex) {
            if (!IsRealized(dataIndex)) {
                int realizationIndex = RealizationIndex(dataIndex);
                Debug.Assert(dataIndex == m_firstRealizedDataIndex - 1 ||
                    dataIndex == m_firstRealizedDataIndex + m_realizedElementBounds.Count ||
                    m_realizedElementBounds.Count == 0);

                if (realizationIndex == -1) {
                    m_realizedElementBounds.Insert(0, new Rect());
                } else {
                    m_realizedElementBounds.Add(new Rect());
                }

                if (m_firstRealizedDataIndex > dataIndex) {
                    m_firstRealizedDataIndex = dataIndex;
                }

                return true;
            }

            return false;
        }

        // Figure out the extent of the layout by getting the number of items remaining
        // above and below the realized elements and getting an estimation based on
        // average item heights seen so far.
        private Rect EstimateExtent(VirtualizingLayoutContext context, Size availableSize) {
            double averageHeight = m_totalHeightForEstimation / m_numItemsUsedForEstimation;

            Rect extent = new Rect(0, 0, availableSize.Width, context.ItemCount * averageHeight);

            if (context.ItemCount > 0 && m_realizedElementBounds.Count > 0) {
                extent.Y = m_firstRealizedDataIndex == 0 ?
                                m_realizedElementBounds[0].Y :
                                m_realizedElementBounds[0].Y - (m_firstRealizedDataIndex - 1) * averageHeight;

                int lastRealizedIndex = m_firstRealizedDataIndex + m_realizedElementBounds.Count;
                if (lastRealizedIndex == context.ItemCount - 1) {
                    var lastBounds = m_realizedElementBounds[m_realizedElementBounds.Count - 1];
                    extent.Y = lastBounds.Bottom;
                } else {
                    var lastBounds = m_realizedElementBounds[m_realizedElementBounds.Count - 1];
                    int lastRealizedDataIndex = m_firstRealizedDataIndex + m_realizedElementBounds.Count;
                    int numItemsAfterLastRealizedIndex = context.ItemCount - lastRealizedDataIndex;
                    extent.Height = lastBounds.Bottom + numItemsAfterLastRealizedIndex * averageHeight - extent.Y;
                }
            }

            DebugTrace("Extent " + extent + " with average height " + averageHeight);
            return extent;
        }

        private bool IsRealized(int dataIndex) {
            int realizationIndex = dataIndex - m_firstRealizedDataIndex;
            return realizationIndex >= 0 && realizationIndex < m_realizedElementBounds.Count;
        }

        // Index in the m_realizedElementBounds collection
        private int RealizationIndex(int dataIndex) {
            return dataIndex - m_firstRealizedDataIndex;
        }

        private void OnItemsAdded(int index, int count) {
            // Using the old indexes here (before it was updated by the collection change)
            // if the insert data index is between the first and last realized data index, we need
            // to insert items.
            int lastRealizedDataIndex = m_firstRealizedDataIndex + m_realizedElementBounds.Count - 1;
            int newStartingIndex = index;
            if (newStartingIndex > m_firstRealizedDataIndex &&
                newStartingIndex <= lastRealizedDataIndex) {
                // Inserted within the realized range
                int insertRangeStartIndex = newStartingIndex - m_firstRealizedDataIndex;
                for (int i = 0; i < count; i++) {
                    // Insert null (sentinel) here instead of an element, that way we do not
                    // end up creating a lot of elements only to be thrown out in the next layout.
                    int insertRangeIndex = insertRangeStartIndex + i;
                    int dataIndex = newStartingIndex + i;
                    // This is to keep the contiguousness of the mapping
                    m_realizedElementBounds.Insert(insertRangeIndex, new Rect());
                }
            } else if (index <= m_firstRealizedDataIndex) {
                // Items were inserted before the realized range.
                // We need to update m_firstRealizedDataIndex;
                m_firstRealizedDataIndex += count;
            }
        }

        private void OnItemsRemoved(int index, int count) {
            int lastRealizedDataIndex = m_firstRealizedDataIndex + m_realizedElementBounds.Count - 1;
            int startIndex = Math.Max(m_firstRealizedDataIndex, index);
            int endIndex = Math.Min(lastRealizedDataIndex, index + count - 1);
            bool removeAffectsFirstRealizedDataIndex = (index <= m_firstRealizedDataIndex);

            if (endIndex >= startIndex) {
                ClearRealizedRange(RealizationIndex(startIndex), endIndex - startIndex + 1);
            }

            if (removeAffectsFirstRealizedDataIndex &&
                m_firstRealizedDataIndex != -1) {
                m_firstRealizedDataIndex -= count;
            }
        }

        private void ClearRealizedRange(int startRealizedIndex, int count) {
            m_realizedElementBounds.RemoveRange(startRealizedIndex, count);
            if (startRealizedIndex == 0) {
                m_firstRealizedDataIndex = m_realizedElementBounds.Count == 0 ? 0 : m_firstRealizedDataIndex + count;
            }
        }

        private void ClearRealizedRange() {
            m_realizedElementBounds.Clear();
            m_firstRealizedDataIndex = 0;
        }

        private Rect GetCachedBoundsForDataIndex(int dataIndex) {
            return m_realizedElementBounds[RealizationIndex(dataIndex)];
        }

        private void SetCachedBoundsForDataIndex(int dataIndex, Rect bounds) {
            m_realizedElementBounds[RealizationIndex(dataIndex)] = bounds;
        }

        private Rect GetCachedBoundsForRealizationIndex(int relativeIndex) {
            return m_realizedElementBounds[relativeIndex];
        }

        void DebugTrace(string message, params object[] args) {
            Debug.WriteLine(message, args);
        }
    }
}
