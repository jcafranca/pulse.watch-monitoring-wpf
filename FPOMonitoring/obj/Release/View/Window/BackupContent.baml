   M S B A M L   `   `   `  ���� M  IDLDSMonitoringv1.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:FPOMonitoring.View.WindowFPOMonitoring.View.Window  -    'FPOMonitoring.View.Window.BackupContent   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     9local'clr-namespace:FPOMonitoring.View.Window     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    � WindowStartupLocation$  CenterOwner=�   RegionBrush#��[  $	��380��$	��550�� � 
ResizeMode$ CanMinimize=� � Title$ Backup Data�� � DataContext ��   Locator �� Source# [  �� Path$ BackupVM�.���� $��10q�.+��  � RowDefinitions �� $
��Auto ��� .+�'  !HandyControl.Controls.SimplePanel  .+��� 
������ $��0q�$��0q�
������ ���� $��Table to Export��$��SemiBold"�.���� ._�/  )HandyControl.Controls.UniformSpacingPanel     Orientation$ Vertical=�   Spacing$ 10Q�.+�*  $HandyControl.Controls.WaterfallPanel  	   Groups$	 2��.+�&   System.Windows.Controls.DataGrid  -   
  Name$
 	Datagrid1��
������ $	��180��$��1q�)  #HandyControl.Controls.BorderElement   CornerRadius$ 10��*  $HandyControl.Controls.DataGridAttach"   CanUnselectAllWithBlankArea .    HeadersVisibility$	 All=�   
IsReadOnly .    RowHeaderWidth$ 30Q�   	RowHeight$ 30Q�$   HorizontalScrollBarVisibility$ Visible=�   SelectionMode$ Extended=�   AutoGenerateColumns .  ���� *Productions+ �� UpdateSourceTrigger$ PropertyChanged=�   RowHeaderTemplate �� .R���  d� 	IsChecked �� *
IsSelected+ �� RelativeSource ��  �� AncestorType$ DataGridRowL�   Columns 0  *System.Windows.Controls.DataGridTextColumn  1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *date+,	  &System.Windows.Controls.DataGridColumn 	  Header �� $��SemiBold"�$��
Production��  -   $
 	Datagrid2��
������ $	��180��$��10 0 0 0q�$��1q�$ 10�� .    ShowSelectAllButton .  $	 All=� . $ 30Q�$ Visible=�$ Extended=� .  ���� *Datas+$ PropertyChanged=�    	  Width5
  /System.Windows.Controls.DataGridLengthConverter$
 Auto
  �� *	prod_id+ �� $��SemiBold"�$
��Date��  $
 Auto
  �� *	Pronumber+ �� $��SemiBold"�$��	Pronumber��  $
 Auto
  �� *Status+ �� $��SemiBold"�$��Status��]� .+�  $ 
Horizontal=�$ 10Q�$
��Left=�.+��� ���� �� $	���  FontAwesome#��[  �� $�Refresh���� $��0 5 0 0q��� $�0�� �� $�production date selected��  $��Right=�$ 5Q�$��Right=�.+���   ButtonPrimary#��[ ���� �� $	���#��[  �� $�Start Backup���  � Value$	 100Q�$��SemiBold"�  PrimaryForeground#��[ �� $��SemiBold"��� $�Status :�� �� $
�None��