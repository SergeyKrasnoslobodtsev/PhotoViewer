   M S B A M L   `   `   `  ����J  FPhotoViewer.App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:PhotoViewer.App.ControlsPhotoViewer.App.Controls  5      +    %PhotoViewer.App.Views.GroupsThumbnail   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;controls&clr-namespace:PhotoViewer.App.Controls     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5        � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $; 5/PhotoViewer.App;component/Styles/MiniatureStyle.xaml?�6%   %    5      .��5      � -   5         �Name$ stack��5      $��5q�6    � ColumnDefinitions 5   
   �� 5      $
��auto �6   �� 5       � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      .+�5   
   d� -   5         d�Name$ tg_group��5       d� IsThreeState .     GroupCircleButton#��[  6>   $
��10 0q�6i    d� 	IsChecked 6w   �� *IsSelectedGroup+�� 5   
   $��1��6     BaseTextBlockStyle#��[ 6$   $��0,8,0,5q�6P   ��6a   �� *Name+  DateToStringConverters	 �� 	Converter#	 [ �� 5   
   -   5        
 ��Name$

 list��5      $��2��6!   $��1��65   $��Disabled=�5      $��Disabled=�5       ��5      �� *Preferences+ �� IsAsync . ��5!      �� 5"      .R�5#      � 5$      ��5&      �� 5'       �� 
TargetType" ��6    �� Triggers 5(      �� 5)       �� Binding 6&   �� *	IsChecked+ �� ElementName$ tg_group�� �� Value True6Z   .a�5*      ��  �� Property �@��
IsSelected �� Value . 6;   5+      .�5-      ��  � �� 5.      �� 5/      .R�50   "   (   "PhotoViewer.App.Controls.Thumbnail  51      53      54      56      57      58      