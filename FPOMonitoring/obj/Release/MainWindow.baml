   M S B A M L   `   `   `  ���� M  IDLDSMonitoringv1.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:FPOMonitoringFPOMonitoring      FPOMonitoring.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:FPOMonitoring     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  -     � WindowState$  	Maximized=� 
  Nunito#��[  $��SemiBold"� � Title$ 
Monitoring�� � Icon$ /DDC-Group-logo1-8.ico��"  HandyControl.Controls.Window   	ShowTitle .  $	��500��$
��1300�� � DataContext ��   Locator �� Source# [  �� Path$
 Main�.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+���   RegionBrush#���  ._�]� 
������ $��	10 5 0 10q�.+��� $
��Left=�.+�/  )HandyControl.Controls.UniformSpacingPanel     Orientation$ 
Horizontal=�	   Spacing$	 10Q�.+�$  HandyControl.Controls.ComboBox  (  "HandyControl.Controls.TitleElement
   Title$
 Show entries��   TitlePlacement$
 Left=�$	��220������ *	ShowLimit+ �� SelectedItem �� *SelectedItem+  -     Name$ ProductionCB��$
 
Production��$
 Left=� �� DisplayMemberPath$
 date��$	��180������ *ProductionDates+ �� *SelectedProdDate+  $	 1Q�.+���   TextBoxExtend#��[ $	��200��$
 Status��$
 Left=� �� 
IsReadOnly . '  !HandyControl.Controls.InfoElement   Placeholder$ Select Filter������ *
StatusText+�� -    ��Name$ 	StatusBTN��  FontAwesome#��[ $	����
������ � -   $��StatusFiltered�� � AllowsTransparency .  � HorizontalOffset$
 -163�� � VerticalOffset$ 6�� � 	StaysOpen .   � PlacementTarget ��  �� ElementName$ 	StatusBTN��.��(  "HandyControl.Controls.ScrollViewer  $
��Auto=�$��16q�$	��180��.���� #���   �� CornerRadius$ 4��$��1q�  BorderBrush#��[ ._���  �� Orientation$ Vertical=�$��5q�.+�(  "HandyControl.Controls.ElementGroup  $	��5 0q�   Orientation$ Vertical=�$	��Top=����� *FilteredStatus+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ OneWay=����� .R��� -   $��10 5 0 0q�$��SemiBold"�$��13(����� *Name+ d� 	IsChecked �� *	IsChecked+$ PropertyChanged=�$ TwoWay=��� 
������ $��0q����� �� $��SemiBold"�$�	Ongoing :�� �� $
��Bold"�
����  ���� *OngoingCount+ �� FallbackValue$ 0�� �� TargetNullValue$ 0���� 
������ $��0q����� �� $��SemiBold"�$�Billed :�� �� $
��Bold"�
����  ���� *BilledCount+$ 0��$ 0���� 
������ $��0q����� �� $��SemiBold"�$�Reject :�� �� $
��Bold"�
����  ���� *RejectCount+$ 0��$ 0���� 
������ $��0q����� �� $��SemiBold"�$�Query :�� �� $
��Bold"�
����  ���� *
QueryCount+$ 0��$ 0���� 
������ $��0q����� �� $��SemiBold"�$�
Answered :�� �� $
��Bold"�
����  ���� *AnsweredCount+$ 0��$ 0���� $��Right=�$��Right=�$��0 0 10 0q�.+�%  HandyControl.Controls.SearchBar  -      Name$  	SearchBar��  SearchBarPlus#��[ $
 Search��$
 Left=�$ Quick Search Ctrl + Q��!   ShowClearButton! . $��SemiBold"�$	��250��"   
IsRealTime" . ���� *
SearchText+$ PropertyChanged=��� $��1��._�]� ���#404E67.+�  $	 10Q�$
��Left=�.+��� 
������ 
������ $��10 0 0 0q����� �� 
�������$�START :�� �� $��SemiBold"�
���������� *Start+$
 None��$
 None���� 
������ 
������ $��0q����� �� 
�������$�END :�� �� $��SemiBold"�
���������� *End+$
 None��$
 None���� 
������ 
������ $��0q����� �� 
�������$�TARGET :�� �� $��SemiBold"�
���������� *Target+$ 0��$ 0���� 
������ 
������ $��0q����� �� 
�������$�BILLED :�� �� $��SemiBold"�
���������� *BilledCount+$ 0��$ 0���� 
������ $��0q���Z� ���� �� 
�������$�
FOR BILL :�� �� $��SemiBold"�
���������� *	ForBilled+$ 0��$ 0��  $��Right=�$	 10Q�$��Right=�$��0 0 10 0q�.+��� 
������ 
������ $��0q����� �� 
�������$"�Production Completion Time :�� �� $��SemiBold"�
���������� *CompletionTime+$ 00:00:00��$ 00:00:00���� 
������ 
������ $��0q����� �� 
�������$�Production HRS :�� �� $��SemiBold"�
��� ������ *ProductionHrs+$ 00:00:00��$ 00:00:00���� $��2��._�'	  !HandyControl.Controls.SimplePanel	  .+�"
  HandyControl.Controls.Drawer
  -   # 
 Name$# DrawerAddUser��$ 
  Dock$	$ Top=�% 
  MaskCanClose% . & K� ZIndex$& 1��' 
  Content.' ��   
BorderClip#��[ $
��0 20q�$ 5��#���  $
��Auto��$	��350��._��  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -   ( ��Name$( Header��._�,  &HandyControl.Controls.SimpleStackPanel  $��
20 20 20 0q�.+�	  .+��� $��User Details��$��20(�$��SemiBold"��� -	   $	����$��16(�
������ 
������ #��[ $��Right=��� $>��8Before saving, double-check the information you entered.��$��Bottom=��� -
   $
( Body��$��2��$��20q�._�  .+��� -   ) ��Name$) 
FullnameTB��$��14(�#��[ $
 Fullname��*   	Necessary* . *  $HandyControl.Controls.WaterfallPanel  $��0 5 0 0q�.+��� -   $) 
UsernameTB��+ �� CharacterCasing$+ Upper=�$��14(�$��0 0 5 0q�#��[ $
 Username��* . '  !HandyControl.Controls.PasswordBox  -   ,  Name$, 
PasswordPB��$
 Password��$��14(�$��5 0 0 0q�* . -   ShowEyeButton- .    $��0 5 0 0q�.+��� -   . ��Name$. GroupCB��$��14(�$��0 0 5 0q�  ComboBoxExtend#��[ $
 Group��$ Select Group���� -   $. BatchCB��$��14(�$��5 0 0 0q�#��[ $
 Batch��$ Select Batch���� -   $. TypeCB��$
 Name��#��[ $��0 5 0 0q�$��14(�$

 Type��* . $ Select User Type���� $��3���� -   $( Footer��$��4��$��20q�._�  $��Right=�$	 5Q�.+��� -   $
��Auto��$
��Auto��$��17(�$��Close�����#6c757d���#6c757d
�������d� -   / d�Name$/ Submit�� 	 ToggleButtonLoading#��[	 $
��Auto��$
��Auto��$��Save changes��$��16(����#fff���#007bff���#007bff
  -   $# DrawerViewProcess��$$ Right=�% . $& 1��0 
  IsOpen0 �� *IsOpen+.' �  �� $
��Auto ��� $��* �.+��� -   $��BorderTitle��$��32��$ 10 0 0 0�� 
 
TitleBrush#��� 
 #��[ ._�	  .+�%  HandyControl.Controls.Watermark  1   Mark$1 FPO Monitoring��2   
MarkMargin$	2 3,0q�$��20(�$
��Bold"�3   	MarkBrush3 �	#0F000000�� -   $��ContentTitle��  TextBlockDefault#��[ 
�������$
��Bold"�$��Biller's Production Data��$��15(��� -   $��
BorderCode��#��[ $��2��$ 0 0 0 10��#���  $	��500��._�� $��10q� �� $��* ��� $
��Auto �.+�&   System.Windows.Controls.DataGrid  -   4  Name$4 FilteredItems��
������ *  $HandyControl.Controls.DataGridAttach"5   CanUnselectAllWithBlankArea5 . 6   HeadersVisibility$	6 All=�7   ShowSelectAllButton7 .  8   
IsReadOnly8 . 9   	RowHeight$9 30Q�$:   HorizontalScrollBarVisibility$: Visible=�;   SelectionMode$; Extended=�<   AutoGenerateColumns< .  
������ =   Columns= 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn>   Width5  /System.Windows.Controls.DataGridLengthConverter$> 50 1  +System.Windows.Controls.DataGridBoundColumn?   Binding? �� *Id+@   Header@ �� $��Id��$��SemiBold"�  ? �� *	Pronumber+@ �� $��	Pronumber��$��SemiBold"�  ? �� *Status+@ �� $��Status��$��SemiBold"�  ? �� *
start_time+@ �� $��
Start Time��$��SemiBold"�  ? �� *
end_time+@ �� $��End Time��$��SemiBold"��� $��Center=�.+��� -   A ��Name$A 
lbl_nodata��$ 
Horizontal=�$��Center=�$��30q�.+���   PrimaryForeground#��[ $��20(�$	����#��[ ��   SecondaryTextBrush#���  $��No data found��$��10 0 0 0q�$��Center=�$��SemiBold"��� $��1��$��
10 10 0 10q�.+��� �� $�Total Processed :�� �� -   B ��Name$B FilteredItemsCounts��$
��Bold"�$�0���� -   C ��Name$C 
TabControl��D �� TabStripPlacement$D Bottom=�.@��� {�.  E �� 
IsSelectedE .  $��SemiBold"����� �� $	���#��[  �� $� Sheets���� $��SemiBold"�$��USERS��.���  �� $��* ��� $
��Auto �.+�  -   $4 UsersDGV��
������ $��0 0 0 1q�)  #HandyControl.Controls.BorderElementF   CornerRadius$F 0��5 . $	6 All=�8 . G   RowHeaderWidth$G 30Q�$9 30Q�$: Visible=�$; Single=�< .  H   FrozenColumnCount$H 1������ *DataSourceProduction+$ PropertyChanged=�I   RowHeaderTemplateI �� .R���  �� *
IsSelected+J �� RelativeSourceJ �� K �� AncestorType$K DataGridRowL�= 4  .System.Windows.Controls.DataGridTemplateColumn  $> 90 L   CanUserReorderL .  M   CanUserSortM .  N   CanUserResizeN .  @ �� -   $	��Add��$��75�����#fff���#007bff���#007bff$��	-10 0 0 0q�$��Right=�O   CellTemplateO �� .R��� $ 
Horizontal=�$��	-10 0 0 0q�.+���   ButtonDefault.Small#��[ 
������ 
������   SuccessBrush#��[ P � ToolTip$P View all process data��$��20��$��0q�$	����#��[ $��23������ *ViewProcessCommand+���� $ UsersDGV��$ SelectedItem��� $ EditUser��-   #��[ $��-5 0 0 0q�
������ 
������   WarningBrush#��[ $��0q�$��23��$
��Bold"�$P Edit user details��$	����#��[ �� #��[ $��-4 0 0 0q�
������ 
������   DangerBrush#��[ $��0q�$��23��$
��Bold"�$P Delete user account��$	����#��[ ���� *DeleteCommand+���� $ UsersDGV��$ SelectedItem��� #��[ $��-4 0 0 0q�
������ 
������ #��[ $��0q�$��23��$
��Bold"�$P Force to logout user��$	����#��[ ���� *ForceLogoutCommand+���� $ UsersDGV��$ SelectedItem�  Q   SortMemberPath$Q Fullname��@ �� $��Fullname��$
��Bold"�O �� .R�.  (HandyControl.Controls.HighlightTextBlock  $
��Bold"�R   
SourceTextR �� *
Fullname+S   QueriesTextS �� $ 	SearchBar��$
 Text�  $Q Username��@ �� $ 
Horizontal=�$��Center=�.+��� $��18(�$	����#��[ �� $��	 Username��$
��Bold"�O �� .R�  $
��Bold"�R �� *
Username+S �� $ 	SearchBar��$
 Text�  ? �� *
BillerCode+@ �� $��Biller Code��$
��Bold"�  ? �� *Job+@ �� $	��Job��$
��Bold"�  ? �� *
BillType+@ �� $��	Bill Type��$
��Bold"�  $Q 	Pronumber��@ �� $ 
Horizontal=�.+��� $��	Pronumber��$
��Bold"��� $��
 (Ongoing)��
��� ���$
��Bold"�O �� .R��� $
��Bold"��� ��� *	Pronumber+  ? �� *Pages+@ �� $��Pages��$
��Bold"�  $Q Status��@ �� $��Status��$
��Bold"�O �� .R��� $��0q�
�������$F 10��$
��10.5(����� *
Foreground+���� *
Foreground+���� *Status+  ? �� *ElapsedTime+@ �� $��Elapsed Time��$
��Bold"�  ? �� *	Remarks+@ �� $��Remarks��$
��Bold"�� $��2��$��1��.+��� $ 
Horizontal=�$��10 15q�.+��� $��SemiBold"��� $�Showing �� �� ��� *
StartIndex+$ OneWay=� �� $
� to �� �� ��� *
EndIndex+$ OneWay=� �� $
� of �� �� ��� *TotalEntries+$ OneWay=� �� $�	 entries ���� $
��Bold"�y�`�   MinValueConverterT `� 	Converter#T [ .1��� $ UsersDGV��$ SelectedItems.Count�$ OneWay=��� $ 0���� ��� $ UsersDGV��$ SelectedItems.Count�$ OneWay=�$ PropertyChanged=� �� $�rows selected��&   HandyControl.Controls.Pagination  U   
AutoHidingU .  $
��10 0q�$��Center=�$��Right=�V   IsJumpEnabledV . W   MaxPageCountW �� *	MaxPage+X   	PageIndexX �� *	PageIndex+$ TwoWay=�,  &HandyControl.Interactivity.InteractionY   TriggersY -  'HandyControl.Interactivity.EventTrigger  Z   	EventName$Z PageUpdated��,  &HandyControl.Interactivity.TriggerBase[   Actions.[ /  )HandyControl.Interactivity.EventToCommand  \   PassEventArgsToCommand\ . ]   Command] �� *PageUpdatedCmd+�� $��BILLED PER HR��$��SemiBold"�.���  �� $��* ��� $
��Auto �.+�  -   $4 BPHDGV��
������ $��0 0 0 1q�$F 0��5 . $	6 All=�8 . $G 60Q�$9 30Q�$: Visible=�$; Extended=�< .  $H 2������ *DataSourceProduction+$ PropertyChanged=�I �� .R��� ^ ��Name$^ 	Checkbox1�� �� *
IsSelected+J �� $K DataGridRowL�=   $Q Fullname��@ �� $��Fullname��$
��Bold"�O �� .R�  $
��Bold"�R �� *
Fullname+S �� $ 	SearchBar��$
 Text�  $Q Username��@ �� $��Username��$
��Bold"�O �� .R�  $
��Bold"�R �� *
Username+S �� $ 	SearchBar��$
 Text�  ? �� *
BillerCode+@ �� $��Biller Code��$��SemiBold"�  $Q 	StartTime��@ �� $��
Start Time��$��SemiBold"�O �� .R��� ���� *	StartTime+y��� *	StartTime+   DateTime2VisibilityConverter_ �� 	Converter#_ [   $Q EndTime��@ �� $��End Time��$��SemiBold"�O �� .R��� ���� *	EndTime+y��� *	EndTime+#_ [   $@ 12:00 AM��O �� .R��� ���� *Hourly[0].billed+y��� *Hourly[0].billed+   Integer2VisibilityConverter2#_ [   $@ 01:00 AM��O �� .R��� ���� *Hourly[1].billed+y��� *Hourly[1].billed+#_ [   $@ 02:00 AM��O �� .R��� ���� *Hourly[2].billed+y��� *Hourly[2].billed+#_ [   $@ 03:00 AM��O �� .R��� ���� *Hourly[3].billed+y��� *Hourly[3].billed+#_ [   $@ 04:00 AM��O �� .R��� ���� *Hourly[4].billed+y��� *Hourly[4].billed+#_ [   $@ 05:00 AM��O �� .R��� ���� *Hourly[5].billed+y��� *Hourly[5].billed+#_ [   $@ 06:00 AM��O �� .R��� ���� *Hourly[6].billed+y��� *Hourly[6].billed+#_ [   $@ 07:00 AM��O �� .R��� ���� *Hourly[7].billed+y��� *Hourly[7].billed+#_ [   $@ 08:00 AM��O �� .R��� ���� *Hourly[8].billed+y��� *Hourly[8].billed+#_ [   $@ 09:00 AM��O �� .R��� ���� *Hourly[9].billed+y��� *Hourly[9].billed+#_ [   $@ 10:00 AM��O �� .R��� ���� *Hourly[10].billed+y��� *Hourly[10].billed+#_ [   $@ 11:00 AM��O �� .R��� ���� *Hourly[11].billed+y��� *Hourly[11].billed+#_ [   $@ 12:00 PM��O �� .R��� ���� *Hourly[12].billed+y��� *Hourly[12].billed+#_ [   $@ 01:00 PM��O �� .R��� ���� *Hourly[13].billed+y��� *Hourly[13].billed+#_ [   $@ 02:00 PM��O �� .R��� ���� *Hourly[14].billed+y��� *Hourly[14].billed+#_ [   $@ 03:00 PM��O �� .R��� ���� *Hourly[15].billed+y��� *Hourly[15].billed+#_ [   $@ 04:00 PM��O �� .R��� ���� *Hourly[16].billed+y��� *Hourly[16].billed+#_ [   $@ 05:00 PM��O �� .R��� ���� *Hourly[17].billed+y��� *Hourly[17].billed+#_ [   $@ 06:00 PM��O �� .R��� ���� *Hourly[18].billed+y��� *Hourly[18].billed+#_ [   $@ 07:00 PM��O �� .R��� ���� *Hourly[19].billed+y��� *Hourly[19].billed+#_ [   $@ 08:00 PM��O �� .R��� ���� *Hourly[20].billed+y��� *Hourly[20].billed+#_ [   $@ 09:00 PM��O �� .R��� ���� *Hourly[21].billed+y��� *Hourly[21].billed+#_ [   $@ 10:00 PM��O �� .R��� ���� *Hourly[22].billed+y��� *Hourly[22].billed+#_ [   $@ 11:00 PM��O �� .R��� ���� *Hourly[23].billed+y��� *Hourly[23].billed+#_ [ � $��1��.+��� $ 
Horizontal=�$��Center=�$��10 15q�.+��� $��SemiBold"��� $�Showing �� �� ��� *
StartIndex+$ OneWay=� �� $
� to �� �� ��� *
EndIndex+$ OneWay=� �� $
� of �� �� ��� *TotalEntries+$ OneWay=� �� $�	 entries ���� $
��Bold"�y�`� #T [ .1��� $ BPHDGV��$ SelectedItems.Count�$ OneWay=��� $ 0���� ��� $ BPHDGV��$ SelectedItems.Count�$ OneWay=�$ PropertyChanged=� �� $�rows selected��  U .  $
��10 0q�$��Center=�$��Right=�V . W �� *	MaxPage+$ 0��X �� *	PageIndex+$ 0��$ 0��Y   $Z PageUpdated��.[   \ . ] �� *PageUpdatedCmd+�� $��SUMMARY��$��SemiBold"�.���  �� $��* ��� $
��Auto �.+�  -    $4 
SUMMARYDGV��
������ $��0 0 0 1q�$F 0��5 . $	6 All=�8 . $G 60Q�$9 30Q�$: Visible=�$; Extended=�< .  $H 2������ *DataSourceProduction+$ PropertyChanged=�I �� .R���  �� *
IsSelected+J �� $K DataGridRowL�=   $Q Fullname��@ �� $��Fullname��$
��Bold"�O �� .R�  $
��Bold"�R �� *
Fullname+S �� $ 	SearchBar��$
 Text�  $Q Username��@ �� $��Username��$
��Bold"�O �� .R�  $
��Bold"�R �� *
Username+S �� $ 	SearchBar��$
 Text�  ? �� *
BillerCode+@ �� $��Biller Code��$
��Bold"�  $Q 	StartTime��@ �� $��
Start Time��$
��Bold"�O �� .R��� ���� *	StartTime+y��� *	StartTime+#_ [   $Q EndTime��@ �� $��End Time��$
��Bold"�O �� .R��� ���� *	EndTime+y��� *	EndTime+#_ [   $Q BilledCount��@ �� $��Billed��$
��Bold"�O �� .R��� ���� *BilledCount+y��� *BilledCount+#_ [   $Q RejectCount��@ �� $��Reject��$
��Bold"�O �� .R��� ���� *RejectCount+y��� *RejectCount+#_ [   $	Q BPH��@ �� $	��BPH��$
��Bold"�O �� .R��� ���� *BPH+y��� *BPH+#_ [   $	> 150 $Q ProdTimeDecimal��@ �� $��Prod Time in Decimal��$
��Bold"�O �� .R��� ���� *ProdTimeDecimal+y��� *ProdTimeDecimal+#_ [   `   MinWidth$	` 105Q�? �� *
ProdTime+$ PropertyChanged=�@ �� $��	Prod Time��$
��Bold"�  $	> 120 ? �� *	MealBreak+$ PropertyChanged=�@ �� $��Meal Break Time��$
��Bold"�  $	> 130 ? �� *NatureBreak+$ PropertyChanged=�@ �� $��Nature Break Time��$
��Bold"�  $	> 110 ? �� *	IdleBreak+@ �� $��Idle Break Time��$
��Bold"�  a   
FontWeight$
a Bold"�? �� *	Remarks+@ �� $��Remarks��$
��Bold"�� $��1��.+��� $ 
Horizontal=�$��Center=�$��10 15q�.+��� $��SemiBold"��� $�Showing �� �� ��� *
StartIndex+$ OneWay=� �� $
� to �� �� ��� *
EndIndex+$ OneWay=� �� $
� of �� �� ��� *TotalEntries+$ OneWay=� �� $�	 entries ���� $
��Bold"�y�`� #T [ .1��� $ 
SUMMARYDGV��$ SelectedItems.Count�$ OneWay=��� $ 0���� ��� $ 
SUMMARYDGV��$ SelectedItems.Count�$ OneWay=�$ PropertyChanged=� �� $�rows selected��  U .  $
��10 0q�$��Center=�$��Right=�V . W �� *	MaxPage+$ 0��X �� *	PageIndex+$ 0��$ 0��Y   $Z PageUpdated��.[   \ . ] �� *PageUpdatedCmd+�� $��
PRODUCTION��$��SemiBold"�.���  �� $��* ��� $
��Auto �.+�	  .+�  -!   $4 ProdDGV��
������ $F 0��
������ 5 . $	6 All=�8 . $G 60Q�$9 30Q�$: Visible=�$; Extended=�< .  ���� *	Documents+$ PropertyChanged=�I �� .R���  �� *
IsSelected+J �� $K DataGridRowL�=   $> 48 L .  M .  N .  O �� .R��� $ 
Horizontal=�$��	-30 0 0 0q�.+��� #��[ 
������ 
������ #��[ $��0q�$��23��$
��Bold"�$P Force to logout user��$	����#��[ ���� *DeleteCommand+���� $ ProdDGV��$ SelectedItem�  ? �� *	Pronumber+@ �� $��	Pronumber��$��SemiBold"�  ? �� *
BillType+@ �� $��BOL Type��$��SemiBold"�  ? �� *BillerUsername+@ �� $��Biller Username��$��SemiBold"�  ? �� *Status+@ �� $��Status��$��SemiBold"��� $��Center=�.+��� $ 
Horizontal=�$��Center=�$��30q�y��� $ ProdDGV��$ Items.Count�  Integer2VisibilityConverter#_ [ .+��� #��[ $��20(�$	����#��[ �� #���  $��No data found��$��10 0 0 0q�$��Center=�$��SemiBold"��� $��1��$��0 1 0 0q�#��[ ._�� .+��� $ 
Horizontal=�$��Center=�$��10 15q�.+��� $��SemiBold"��� $�Showing �� �� ��� *ProdStartIndex+$ OneWay=� �� $
� to �� �� ��� *ProdEndIndex+$ OneWay=� �� $
� of �� �� ��� *ProdTotalEntries+$ OneWay=� �� $�	 entries ���� $
��Bold"�y�`� #T [ .1��� $ ProdDGV��$ SelectedItems.Count�$ OneWay=��� $ 0���� ��� $ ProdDGV��$ SelectedItems.Count�$ OneWay=�$ PropertyChanged=� �� $�rows selected��  U .  $
��10 0q�$��Center=�$��Right=�V . W �� *ProdMaxPage+$ 0��X �� *ProdPageIndex+$ 0��$ 0��Y   $Z PageUpdated��.[   \ . ] �� *PageUpdatedCmd+�� $��3��._�]� ���#404E67.+�  $	 10Q�$
��Left=�.+��� 
������ 
������ $��10 0 0 0q����� �� 
�������$�Server :�� �� $��SemiBold"�
���������� *Server+$ 000.000.000.000 : 0000��$ 000.000.000.000 : 0000���� 
������ 
������ $��0q����� �� 
�������$�
DateTime :�� �� 
�������$	���#��[  �� $��SemiBold"�
���������� *DateAndTime+$
 None��$
 None��  $��Right=�$	 10Q�$��Right=�$��0 0 10 0q�.+��� 
������ 
������ $��0q����� -"   b ��Name$b iconTextBlock��
����������� c �� 
TargetType"c ��d �� Triggersd �� e �� Bindinge �� *	IsRunning+f �� Valuef Trueg N� EnterActionsg �� h ��Name$h HeartBeatAnimation��.���� i g� RepeatBehavior$i Forever��.��W� j �� TargetProperty$j Opacity�k W� From$k 0X�	l W� To$l 1X�m g� Duration$m 0:0:0.5D�W� $j Opacity�$k 1X�$l 0X�$m 0:0:0.5D�n g� 	BeginTime$n 0:0:0.5X�o N� ExitActionso �� p �� BeginStoryboardName$p HeartBeatAnimation���� #��[ ��� # [ $ CommandVM.Icon� �� $��SemiBold"���� # [ $ CommandVM.StatusMessage� �� $	�...��