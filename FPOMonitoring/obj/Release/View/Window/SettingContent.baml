   M S B A M L   `   `   `  ���� M  IDLDSMonitoringv1.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:FPOMonitoring.View.WindowFPOMonitoring.View.Window  .    (FPOMonitoring.View.Window.SettingContent   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     9local'clr-namespace:FPOMonitoring.View.Window     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    � Title$  Setting Informations��$	��415��$	��500��   RegionBrush#��[   
 Nunito#��[ $��SemiBold"� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterOwner=� � DataContext ��   Locator �� Source# [  �� Path$ 	SettingVM� +� FocusedElement ��  �� ElementName$ 
HostnameTB�� � 	Resources   TagCustomStyle  TagBaseStyle	 �� BasedOn
 �� 
TargetType  HandyControl.Controls.Tag �� Property �� Value��%5   &	       7  �� 8	   "
  .���  � ��$ 3q�.��� $
��10 0q� � ColumnDefinitions �� �� $
��Auto ��� .+��� {��� *	Loading+  ReverseBooleanConverter �� 	Converter# [ .+��� $��0 0 0 2q�.+�(  "HandyControl.Controls.TagContainer  #��[ #���   $��0 15 0 5q�$��85��$��5q����� *	Hostnames+]� ��. .+���   PrimaryBrush#���  #���  
�������$��SemiBold"�$��5 0 0 0q�$��Right=�$	��Add������ *AddItemCommand+#  HandyControl.Controls.TextBox  -     Name$ 
HostnameTB��'  !HandyControl.Controls.InfoElement   Placeholder$ Hostname��   ShowClearButton . ���� *	TagName+   . $  ��(  "HandyControl.Controls.TitleElement   TitlePlacement$	 Top=�   Title$
 Port��   	Necessary . ���� *Port+   . $  ��$	 Top=�$ Database Name�� . ���� *DatabaseName+   . $  ��$	 Top=�$ Username�� . ���� *
Username+'  !HandyControl.Controls.PasswordBox  -   $��TextBoxPassword��$ Password�� . $  �� .    ShowEyeButton . d� -   $��ButtonLogin��  ToggleButtonLoadingPrimary#��[ $��30��$��SemiBold"�$��0q�$��0 10 0 0q�$��Right=�$��Test Connection��{��� *	Loading+ �� Mode$ TwoWay=� �� FallbackValue$
 true�� �� TargetNullValue$
 true��# [  d� 	IsChecked �� *	Loading+$ TwoWay=�$ false��$ false������ *TestCommand+��   PrimaryForeground#��[ $
��10 0q�$��1��$��3�� �� ResizeBehavior$ PreviousAndNext=� �� ResizeDirection$ Columns=��� $��2��.+�   . $  ��$	 Top=�$ Start�� . ���� *Start+   . $  ��$	 Top=�$	 End�� . ���� *End+�� -    ��Name$
 Note��$��0 5 0 0 q�$��SemiBold"�#��[  �� TextAlignment$
 Left=�(&Note: Start and End must be in PH time�� 24Hr Format (HH:mm).,  &HandyControl.Controls.SimpleStackPanel     Orientation$ 
Horizontal=�$��0 10 0 0q�$��Right=�.+�/  )HandyControl.Controls.UniformSpacingPanel     Spacing$ 5Q�.+��� -   $��Cancel�� 	 ButtonDanger#��[	 d� -   $��
ButtonSave��#��[ $��30��$��SemiBold"�$��0q�$��Right=�$
��Save��{��� *
Loading2+$ TwoWay=�$
 true��$
 true��# [  �� *
Loading2+$ TwoWay=�$ false��$ false������ *SaveCommand+