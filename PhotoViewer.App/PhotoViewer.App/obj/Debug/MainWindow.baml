   M S B A M L   `   `   `  ����  PhotoViewer.App     PhotoViewer.App.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   M IModernWpf.Controls, Version=0.9.3.0, Culture=neutral, PublicKeyToken=nullD @ModernWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null,ui!http://schemas.modernwpf.com/2019   5   	   0  *ModernWpf.Controls.Primitives.WindowHelper    UseModernWindowStyle  .  � Title$ 
MainWindow��5   	   $��648.506��6   $��902.212��6-    � DataContext 5
   	   �� *Main+   Locator �� Source# [   � 	Resources 5         BooleanToVisibilityConverter��%   &	       �� 5   
   6'   5      .��5      � .+�5   
   '  !ModernWpf.Controls.NavigationView  -   5          Name$	 Nav��5         PaneDisplayMode$
 Left=�6)   #   IsTitleBarAutoPaddingEnabled .    OpenPaneLength$	 200Q�6d   	   OverflowLabelMode$	 	MoreLabel=�6y   
   IsBackButtonVisible$
 	Collapsed=�6�      
PaneHeader 5      )  #ModernWpf.Controls.SimpleStackPanel  5         Orientation$ 
Horizontal=�6&   .+�5      �� $��40��6   $0��*/PhotoViewer.App;component/Images/Logo.png��6'   �� 5      $��22(�6!   $��Center=�6/   $��Monotype Corsiva)�6J   Photo Viewer6w   5         	MenuItems 5      +  %ModernWpf.Controls.NavigationViewItem  5      $��Фото��6(   
 � Tag$ Photo��67      Icon 5      !  ModernWpf.Controls.PathIcon  5       	 photo   Data# [ 6&   5        5      $��Альбомы��6(   $ Album��6:    5        5       	 album# [ 6&   5!        5"      $��
Карта��6(   $
 Maps��68    5#        5$        map# [ 6&   5&        5'      $��Для вас��6(    5(        5)       
 foryou# [ 6&   5+        5,      $��
Архив��6(    5-        5.        archive# [ 6&   50        51      $��Корзина��6(    52        53       
 basket# [ 6&   55      4  .ModernWpf.Controls.NavigationViewItemSeparator  56      y�61   ��  �� ElementName$	 Nav�� �� Path$ 
IsPaneOpen� �� 	Converter# [   57      y�6(   �� $	 Nav��$ 
IsPaneOpen�# [ ��58      �� 59      .R�5:        .+�5;   "   �� $��Center=�6,   $
��Bold"�6I     BaseTextBlockStyle#��[ 6[   Хранилище6�   $  ModernWpf.Controls.ProgressBar  5<   "   $��15 10q�61   $��5��6|    � Value 6@   �� *CurrentValue+ � Maximum 6_   �� *
MaxValue+6|   �� 5=   "   $��Center=�6,   $
��15 0q�6I   $
��Wrap=�6W    	 CaptionTextBlockStyle#��[	 6k   ��5>   &   `� 5?   *    �� StringFormat$, & Использовано {0} из {1}��67   .1�5@   .   �� $ AvailableSizeDisk�66   �� 5A   .   $ TotalSizeValueDisk�66   5B   +   5D   #   5E      5F      5H         
PaneFooter 5J      %  ModernWpf.Controls.AppBarButton  5K      $
��test��6"   .��5M      	  ModernWpf.Controls.Frame	  -   5         	 Name$ content��5M      $��Stretch=�6)   $��Stretch=�6G     � NavigationUIVisibility$ Hidden=�6c     � Source$; 5/PhotoViewer.App;component/Views/Photo/PhotoPage.xaml?�6�   5N      5O      5P      