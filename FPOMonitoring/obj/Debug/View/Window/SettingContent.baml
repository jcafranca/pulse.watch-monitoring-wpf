   M S B A M L   `   `   `  ����M  IDLDSMonitoringv1.8, Version=1.8.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:FPOMonitoring.View.WindowFPOMonitoring.View.Window  5      .    (FPOMonitoring.View.Window.SettingContent   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      9local'clr-namespace:FPOMonitoring.View.Window     5      S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5        � Title$  Setting Informations��5	      $	��415��6)   $	��500��66      RegionBrush#��[  5
       
 Nunito#��[ 5      $��SemiBold"�5       � 
ResizeMode$ NoResize=�5       � WindowStartupLocation$ CenterOwner=�5       � DataContext 5      ��   Locator �� Source# [  �� Path$ 	SettingVM� +� FocusedElement 5      ��  �� ElementName$ 
HostnameTB�� � 	Resources 5        TagCustomStyle  TagBaseStyle	 �� BasedOn
 �� 
TargetType  HandyControl.Controls.Tag �� Property �� Value��%b   &	       7  �� 5   
   8	   "
  5      .�5      ��  � ��$ 3q�5      5      5      .��5      � $
��10 0q�6    � ColumnDefinitions 5   
   �� 5      �� 5      $
��Auto �6   �� 5      .+�5!   
   �� {�6   �� *	Loading+  ReverseBooleanConverter �� 	Converter# [ .+�5"      �� $��0 0 0 2q�6   .+�5#      (  "HandyControl.Controls.TagContainer  #��[ 6"   #���   5$   "   $��0 15 0 5q�5%   "   $��85��5&   "   $��5q�5'   "   ��5(   "   �� *	Hostnames+]� 5)      ��. .+�5*      ��   PrimaryBrush#���  5+      #���  5,      
�������$��SemiBold"�5.      $��5 0 0 0q�5/      $��Right=�50      $	��Add��51      ��5*      �� *AddItemCommand+51      #  HandyControl.Controls.TextBox  52      -   5          Name$ 
HostnameTB��52   !   '  !HandyControl.Controls.InfoElement   Placeholder$ Hostname��53   !      ShowClearButton . ��56   !   �� *	TagName+57      58        59       . $  ��5:      (  "HandyControl.Controls.TitleElement   TitlePlacement$	 Top=�5;         Title$
 Port��5<         	Necessary . ��5>      �� *Port+  5?       . $  ��5@      $	 Top=�5A      $ Database Name��5B       . ��5D      �� *DatabaseName+  5E       . $  ��5F      $	 Top=�5G      $ Username��5H       . ��5J      �� *
Username+'  !HandyControl.Controls.PasswordBox  5K      -   5        $��TextBoxPassword��5P      $ Password��5K       . $  ��5M       .    ShowEyeButton . 5S      d� 5T      -   5        $��ButtonLogin��5Z        ToggleButtonLoadingPrimary#��[ 5V      $��30��5W      $��SemiBold"�5X      $��0q�5Y      $��0 10 0 0q�5\      $��Right=�6-   $��Test Connection��6I   {�5T      �� *	Loading+ �� Mode$ TwoWay=� �� FallbackValue$
 true�� �� TargetNullValue$
 true��# [  d� 	IsChecked 5U      �� *	Loading+$ TwoWay=�$ false��$ false����5[      �� *TestCommand+5\   I   5]      �� 5^   
     PrimaryForeground#��[ 6   $
��10 0q�6G   $��1��6U   $��3��6e    �� ResizeBehavior$ PreviousAndNext=�6o    �� ResizeDirection$ Columns=�6�   �� 5_   
   $��2��6   .+�5`         . $  ��5a      $	 Top=�5b      $ Start��5c       . ��5e      �� *Start+  5f       . $  ��5g      $	 Top=�5h      $	 End��5i       . ��5k      �� *End+�� 5m      -   5         ��Name$
 Note��5m   @   $��0 5 0 0 q�6   $��SemiBold"�6*   #��[ 6N    �� TextAlignment$
 Left=�6~   1/Note: Double check the start and end must be in�� 5n   B   24Hr Format (HH:mm).5o      ,  &HandyControl.Controls.SimpleStackPanel  5q         Orientation$ 
Horizontal=�6"   $��0 10 0 0q�6;   $��Right=�6M   .+�5r      /  )HandyControl.Controls.UniformSpacingPanel     Spacing$ 5Q�6)   .+�5s      �� -   5        $��Cancel��5s       	 ButtonDanger#��[	 6.   6T   d� 5t      -   5        $��
ButtonSave��5z   #   #��[ 5v   #   $��30��5w   #   $��SemiBold"�5x   #   $��0q�5y   #   $��Right=�5{   #   $
��Save��5}   #   {�5t   #   �� *
Loading2+$ TwoWay=�$
 true��$
 true��# [  5u   #   �� *
Loading2+$ TwoWay=�$ false��$ false����5|   #   �� *SaveCommand+5}   #   5~      5      5�      5�      5�      