   M S B A M L   `   `   `  ����   BMC.Presentation.POS    BMC.Presentation.CTickets   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     >�Name$  UserControl��UserControl��$	��870��$	��660��.��� -    �Name$ 
LayoutRoot��
LayoutRoot�����	#19FFFFFF � RowDefinitions �� RowDefinition_1��$��0.16* ��� RowDefinition_2��$��0.84* �.+��� -    ��Name$ pnlTicketContent��pnlTicketContent��$��Stretch=�$
��Auto��$��0,0,0,0q�$��1�����	#804F7735$��1,1,1,1q� �� CornerRadius$ 4.5,4.5,4.5,4.5��   	SubFormBG#���   �� -    ��Name$ 
pnlButtons��
pnlButtons��$��0,0,0,-0.974q�$��Bottom=� �� Orientation$ 
Horizontal=�$
��Left=�.+�	� -    	�Name$ chkPrint��chkPrint��  CTickets_xaml_chkPrint#���   " bmc_General_RADIO_Style_Button#���  $��Segoe UI)�$��14(�$
��Bold"�$
��Auto��$
��Auto�� d� 	IsChecked$ FalseY�$��1,0,0,-1q�$��Bottom=�	� -   $ 	chkRedeem��	chkRedeem��$
��Auto��  CTickets_xaml_chkRedeem#���  #���  $��Segoe UI)�$��14(�$
��Bold"�$
��Auto��$��7,0,0,-1q�$��Bottom=�	� -   $ chkMultipleVoucherRedeem��chkMultipleVoucherRedeem��$
��Auto��  MultipleTicketRedeem#���  #���  $��	Segoe UI )�$��14(�$
��Bold"�$
��Auto��$��7,0,0,-1q�$��Bottom=�	� -   $ chkVoid��chkVoid��$
��Auto��  CTickets_chkVoid#���  #���  $��Segoe UI)�$��14(�$
��Bold"�$
��Auto��$��7,0,0,-1q�$��Bottom=�	� -	   $ chkAttendantPays��chkAttendantPays�� " CTickets_xaml_chkAttendantPays#���  #���  $��Segoe UI)�$��14(�$
��Bold"�$
��Auto��$
��Auto��$ FalseY�$��7,0,0,-1q�$��Bottom=�	� -
   $ chkManualAttendantPays��chkManualAttendantPays��$
��Auto�� ' #CTickets_xaml_chkManualAttendantpay#���  #���  $��Segoe UI)�$��14(�$
��Bold"�$
��Auto��$��7,0,0,-1q�$��Bottom=��� TextBlock_1��	 �� TextAlignment$
	 Left=�$��Segoe UI)�$
��Bold"�$��18(�$	��Top=�$
��Left=�$��	25,10,0,0q�
 J� RenderTransformOrigin$
 0.5,0.5&�  CTickets_xaml_TextBlock_1#���  ���	#FFFFFFFFz�V� TransformGroup_1��.~��� ScaleTransform_1�� �� ScaleX$ 1Q� �� ScaleY$ 1Q��� SkewTransform_1�� �� AngleX$ 0Q� �� AngleY$ 0Q��� RotateTransform_1�� �� Angle$ 0Q�T� TranslateTransform_1�� T� X$ 0Q� T� Y$ 0Q�F� 
Path_1�� �� Stretch$
 Fill=�������}?�3-i��JK�3-i�`$SK�3-i0ZK`4i0ZK�u<i�0ZK�u<i�0ZK@�Di`$SK�Ki�JK�Kiѐ�}?�Ki�q=
ף��@�KiP�n?@�DiP�n?�u<i�P�n?�u<i�P�n?`4iq=
ף��@�3-i��}?�3-i$
��Left=�$	��Top=�$��199.975��$��1.998��$��	25,34,0,0q����� LinearGradientBrush_1�� �� 
StartPoint$ -1.22086e-006,0.499939&� �� EndPoint$ 0.999997,0.499939&�.��� GradientStop_1�� � Color$ 	#FFC4CFBE�� � Offset$ 0Q�� GradientStop_2��$ 	#00C4CFBE��$ 1Q�