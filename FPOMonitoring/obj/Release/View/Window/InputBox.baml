   M S B A M L   `   `   `  ���� M  IDLDSMonitoringv1.0, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:FPOMonitoring.View.WindowFPOMonitoring.View.Window  (    "FPOMonitoring.View.Window.InputBox   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     9local'clr-namespace:FPOMonitoring.View.Window     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  -    
  Nunito#��[  $��SemiBold"�  � Title$  
SweetAlert�� � WindowStartupLocation$ CenterOwner=� � WindowStyle$
 None=�
������ $��0q� � 
ResizeMode$ NoResize=� � AllowsTransparency . 
������  � Topmost .  � SizeToContent$ Height=�"  HandyControl.Controls.Window   ShowNonClientArea .   � DataContext ��   Locator	 �� Source#	 [ 
 �� Path$

 Main� � 	Resources  �� MergedDictionaries �� Source��  �� $1 +\resources\animation\animationresource.xaml?�%    .��� .+��� -    ��Name$ _Dialog��$��10q�$	��400��$	��800��$	��800��  RegionBrush#��[  �� CornerRadius$ 5�� �� Padding$ 20q� J� RenderTransformOrigin$ 0.5,0.5&�$��Center=� � Triggers :�  :� RoutedEvent$ Loaded��.[���   ShowAnimation#��[ z�V� .~��� �� �� T�  J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 8Q�   Color$
 #000��   	Direction$	 270Q�   Opacity$	 0.3Q�   ShadowDepth$ 4Q�._���  �� Orientation$ Vertical=�$��Center=�.+�(� $��50��$��50��.���� $��24��$��24��.+�F� ����  �� 
TargetType" F�.���  �� Property � �� �� Value �#C9DAE1��  � ��� ���ا ���@]� ���@]�  $� ��ا  $� ��ا ��� � �� ׀�  �  ��� ��� � � ��O ��� ��� ׀�O �  ��� ��� � � ��  ��� ��� � �  -1�p�s  -1 	= �e�  	=  � � 	= p�s p�s  	=  �  	= Ӑe�  	=  -1p�s  -1 � � -1�e� �e�  -1 �  -1� � ��[ � z ���   	=  	= р�� ��� � �  z ��� �� ׀�� ��� ��� �� Ӏ��  � �ا pJ� �ا ��� �@]� ��� �@]� ���  $�  ��  $� ��� � � ��[   	=  	= 	�� -    ��Name$ _Title��$��Title��$
��Bold"�$
��20pt(�$��Center=�$
��Wrap=�#  HandyControl.Controls.TextBox  -     Name$ _Content��  �� CharacterCasing$  Upper=�$��	0 15 0 10q�  TextBoxExtend#��[ $��Center=�$��Center=�$	��300������ *Target+�� $ 
Horizontal=�$��Center=�$��0 0 0 10q�.+��� -   ! ��Name$! 
ButtonSave��$
��Save��$��0 0 5 0q�  ButtonSuccess#��[ �� -   $! ButtonClose��$��Close��  ButtonDanger#��[ �� $8��2Please make sure that the data you input is valid.��" �� TextAlignment$" Center=�$
��Bold"�