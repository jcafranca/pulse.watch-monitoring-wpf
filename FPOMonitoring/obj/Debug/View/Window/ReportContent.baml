   M S B A M L   `   `   `  ����M  IDLDSMonitoringv1.8, Version=1.8.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:FPOMonitoring.View.WindowFPOMonitoring.View.Window  5      -    'FPOMonitoring.View.Window.ReportContent   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      9local'clr-namespace:FPOMonitoring.View.Window     5      S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5        � WindowStartupLocation$  CenterOwner=�5	       � Title$ Report��5
      $	��350��6   $	��400��6(    � 
ResizeMode$ NoResize=�5         RegionBrush#��[  5       
 Nunito#��[ 5      $��SemiBold"�5       � DataContext 5      ��   Locator �� Source# [  �� Path$ ReportVM�.��5      � $��0 5 0 0q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� ._�5      �� .+�5      /  )HandyControl.Controls.UniformSpacingPanel  $��Center=�6)      Spacing$ 10Q�5   )      Orientation$ 
Horizontal=�5   )   .+�5      ��   ButtonPrimary#��[ 6   {�6D   �� *	IsEnabled+��6d   �� *
Generate+��5      �� 5      �� 5    "   $��14(�6&   $	���64     FontAwesome#��[ 6D    �� 5!   "   $�	 Generate��6&   5"      5$      �� 5&      ��6   �� *BrowseReport+��5'      �� 5(      �� 5)   "   $��16(�6&   $	���64   #��[ 6D    �� 5*   "   $� Browse Report��6&   5+      5-      �� 5/      $	��150��6     ComboBoxExtend#��[ 50      (  "HandyControl.Controls.TitleElement	   Title$
	 Date��51      
   TitlePlacement$

 Left=�52       �� DisplayMemberPath$
 date��54       �� MaxDropDownHeight$	 300��55      ��53      �� *ProductionDates+ �� SelectedItem 56      �� *SelectedDate+58      � 5:      $��5q�6    � ColumnDefinitions 5;      �� 5<      �� 5=      $
��Auto �6+   .+�5?      #  HandyControl.Controls.TextBox  '  !HandyControl.Controls.InfoElement   ShowClearButton .    Placeholder$ Select a path��6G   ��6r   �� *
ReportPath+�� 5@      $��1��6;   $��5 0 0 0q�6K   $	����6\   #��[ 6o   ��6   �� *	BrowseDir+6o   5A      5C      5D      �� 5F   
   $��1��6   $	��5 0q�6   ._�5G        $ Vertical=�6%   $ 10Q�6<   .+�5H      �� $��Details��6   
������ .��5I      �� ._�5J      �� .+�5K      (  "HandyControl.Controls.ElementGroup  $��5 5 0 0q�6.   .@�5L   "   *  $HandyControl.Controls.WaterfallPanel  .+�5M   &     $ Vertical=�6=   $ 5Q�6T   .+�5N   *   ��  J� IsHitTestVisible .  $��Biller Production Report��6L   $��SemiBold"�6o    d� 	IsChecked$
 TrueY�6�   �� 5O   *    .  $��Hourly Production Report��6L   $��SemiBold"�6o   $
 TrueY�6�   �� 5P   *    .  $��Summary Report��6L   $��SemiBold"�6e   $
 TrueY�6{   5Q   '     5R   &   $ Vertical=�6=   $ 5Q�6T   .+�5S   *   ��  .  $��Idle Reports��6L   $��SemiBold"�6c   $
 TrueY�6y   �� 5T   *    .  $��Idle Hourly Report��6L   $��SemiBold"�6i   $
 TrueY�6   �� 5U   *    .  $��Idle Summary Report��6L   $��SemiBold"�6j   $
 TrueY�6�   5V   '   5W   #   5X      � 5Y      $��5 10 5 5q�6#    5Z   "   �� 5[   &   �� 5\   &   $
��Auto �67    5_   "   �� 5`   &   �� 5a   &   5d      5e      5f      5g      � 5h        PrimaryForeground#��[ 6   $��SemiBold"�6i    � Value 6N   �� *
Progress+6i   �� 5i      $��WrapWithOverflow=�6   �� 5j      $�Status :��6   $
��Bold"�5k       �� 5l      $��SemiBold"�5m      �5l      �� *StatusMessage+5m      5n      5o      5p      5q      5r      