   M S B A M L   `   `   `  ����   BMC.Presentation.POS9    3BMC.Presentation.POS.UserControls.TestCashDispenser   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     -     � Title$  ManualCashDispenser��$	��300��$	��300��   MainScreenBG#���   $��Stretch=�$��Center=�$��Segoe UI)�$��14(� � AllowsTransparency .  � WindowStyle$
 None=� � WindowStartupLocation$ CenterOwner=� � 	Resources  & "GroupBoxHeaderAdjustmentFromMargin �� 
TargetType �� Property �� Value �� 
TargetType	 J� SnapsToDevicePixels
 � ColumnDefinitions � RowDefinitions �� CornerRadius   PositionDetailsGroupBoxStyle ��Name �� Padding��%�  &	       �� Style_68��" ��.��� 
Setter_568�� � ��$ 1q��� 
Setter_569�� � �� �� ControlTemplate_75��" ��.R�� Grid_82��	 . 
 �� ColumnDefinition_40��$��6 ��� ColumnDefinition_41��$
��Auto ��� ColumnDefinition_42��$��* ��� ColumnDefinition_43��$��6 � �� RowDefinition_43��$
��Auto ��� RowDefinition_44��$
��Auto ��� RowDefinition_45��$��* ��� RowDefinition_46��$��6 �.+��� 	Border_31��$��4��$��0��$��4�����	#FF161826#��z��$ 4��#���  �� 	Border_32��$��0��$��4��$��1��$��3��#��z��
������ #��z��$ 4���� $ Header��
Header��$��Stretch=�$��0,5,0,0q�$��1��$��2��$��0��$��2�����	#FF161826$��1,1,1,1q�$ 2,2,2,2��$ 0,0,0,0q�#���  $��30��._��� ContentPresenter_5��#	 z	 $��Header����. $��Center=�$��9,0,0,0q��� ContentPresenter_6��#��z��#	 z	 $��1��$��2��$��2��.���� 	Border_10��$ 0,0,0,0�����	#19CDDD98$��0,0,0,0q�._���    TestCashDispenser_xaml_Title#���  $��16(�$
��bold"�#���  $��Stretch=�$��Stretch=�.��� -    �Name$ 
LayoutRoot��
LayoutRoot�����	#19FFFFFF J� RenderTransformOrigin$ 0.5,0.5&� �� $��* ��� $��80 �.+�� $��0��$��Stretch=�$��Stretch=�
 �� $��10 ��� $��80 ��� $
��100* ��� $	��120 ��� $��10 � �� $��50 ��� $��50 ��� $��50 ��� $��* �.+��� -    ��Name$ lblCD1��
lblCD1��$��0��$��1��$
��Auto��$
��Auto��$��Segoe UI)�$��18(�$
��Bold"� �� TextAlignment$ Center=�$
��Wrap=�$��0,10,0,0q�$��Center=�$��Center=��� -    ��Name$ rcCD1Status��rcCD1Status��
��� � �$��0��$��3��$��Stretch=�$��Stretch=�$��5,8,5,8q�$��Center=�$��Center=�
������� J� BitmapEffect ��  �� 
BevelWidth$ 5Q� �� EdgeProfile$ CurvedIn=� �� 
LightAngle$	 320Q� �� Relief$	 0.1Q� �� 
Smoothness$ 1Q��� -   $ 
lblCD1Calc��
lblCD1Calc��$��0��$��2��$
��Auto��$
��Auto��$��Segoe UI)�$��18(�$
��Bold"�$ Center=�$
��Wrap=�$��5,10,5,0q�$��Right=�$��Center=��� -   $ lblCD2��
lblCD2��$��1��$��1��$
��Auto��$
��Auto��$��Segoe UI)�$��18(�$
��Bold"�$ Center=�$
��Wrap=�$��0,5,0,0q�$��Center=�$��Center=��� -   $ rcCD2Status��rcCD2Status��
��� � �$��1��$��3��$��Stretch=�$��Stretch=�$��5,8,5,8q�$��Center=�$��Center=�
������� �� $ 5Q�$ CurvedIn=�$	 320Q�$	 0.1Q�$ 1Q��� -   $ 
lblCD2Calc��
lblCD2Calc��$��1��$��2��$
��Auto��$
��Auto��$��Segoe UI)�$��18(�$
��Bold"�$ Center=�$
��Wrap=�$��5,5,5,0q�$��Right=�$��Center=��� -	   $ lblErrorDesc��lblErrorDesc��$��2��$��5��$
��Auto��$
��Auto��$��Segoe UI)�$��12(�$
��Bold"�$ Center=�$
��Wrap=�$��5,5,5,5q�$��Stretch=�$��Stretch=�� $��1��$��Stretch=�$��Stretch=�
 �� $	��50* ��� $	��150 ��� $	��150 ��� $	��50* �.+��� -
   $��btnTestDispense��$��Segoe UI)�$��14(�$��55�� * &TestCashDispenser_xaml_btnTestDispense#���    
BMC_Button#���  $��1��$��0��$��5,5,5,5q�$��Right=�$��Center=��� -   $��	btnCancel��$��Segoe UI)�$��14(�$��55�� $  TestCashDispenser_xaml_btnCancel#���  #���  $��2��$��0��$��5,5,5,5q�$
��Left=�$��Center=�