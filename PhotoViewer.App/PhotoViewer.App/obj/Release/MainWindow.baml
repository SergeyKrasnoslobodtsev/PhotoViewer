   M S B A M L   `   `   `  ����   PhotoViewer.App     PhotoViewer.App.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     M IModernWpf.Controls, Version=0.9.2.0, Culture=neutral, PublicKeyToken=nullD @ModernWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null,ui!http://schemas.modernwpf.com/2019   0  *ModernWpf.Controls.Primitives.WindowHelper    UseModernWindowStyle  .  � Title$ 
MainWindow��$��648.506��$��902.212�� � DataContext �� *Main+   Locator �� Source# [   � 	Resources    BooleanToVisibilityConverter��%   &	       �� .��� .+�'  !ModernWpf.Controls.NavigationView  -     Name$	 Nav��   PaneDisplayMode$
 Left=�#   IsTitleBarAutoPaddingEnabled .    OpenPaneLength$	 200Q�	   OverflowLabelMode$	 	MoreLabel=�
   IsBackButtonVisible$
 	Collapsed=�   
PaneHeader )  #ModernWpf.Controls.SimpleStackPanel     Orientation$ 
Horizontal=�.+��� $��40��$0��*/PhotoViewer.App;component/Images/Logo.png���� $��22(�$��Center=�$��Monotype Corsiva)�Photo Viewer   	MenuItems +  %ModernWpf.Controls.NavigationViewItem  $��Фото��
 � Tag$ Photo��   Icon !  ModernWpf.Controls.PathIcon   	 photo   Data# [   $��Альбомы��$ Album��    	 album# [   $��
Карта��$
 Maps��     map# [   $��Для вас��    
 foryou# [   $��
Архив��     archive# [   $��Корзина��    
 basket# [ 4  .ModernWpf.Controls.NavigationViewItemSeparator  y���  �� ElementName$	 Nav�� �� Path$ 
IsPaneOpen� �� 	Converter# [   y��� $	 Nav��$ 
IsPaneOpen�# [ ���� .R�  .+��� $��Center=�$
��Bold"�  BaseTextBlockStyle#��[ Хранилище$  ModernWpf.Controls.ProgressBar  $��15 10q�$��5�� � Value �� *CurrentValue+ � Maximum �� *
MaxValue+�� $��Center=�$
��15 0q�$
��Wrap=� 	 CaptionTextBlockStyle#��[	 ��`�  �� StringFormat$, & Использовано {0} из {1}��.1��� $ AvailableSizeDisk��� $ TotalSizeValueDisk�   
PaneFooter %  ModernWpf.Controls.AppBarButton  $
��test��.��	  ModernWpf.Controls.Frame	  -    	 Name$ content��$��Stretch=�$��Stretch=�  � NavigationUIVisibility$ Hidden=�  � Source$; 5/PhotoViewer.App;component/Views/Photo/PhotoPage.xaml?�