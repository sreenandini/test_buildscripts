   M S B A M L   `   `   `  ����   BMC.Presentation.POS?    9BMC.Presentation.POS.Views.CLiquidationDetailsProfitShare   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     �Name$  Window��$��687.716��$	��825�� � AllowsTransparency .  � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=���Z� .��>� H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -    >�Name$ UserControl��UserControl��$	��814�� J� RenderTransformOrigin$ 0.5,0.5&�$��6,0,5,45q� � 	Resources    DateTemplate �� TextAlignment  EventTemplate  DescriptionTemplate��%�  &	        &	 �     &	 �    �� DataTemplate_1��.R�� 
Grid_1��$��0��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��0.5,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_1��$
 Left=�$��0,0,0,0q����� *	EventDate+�� DataTemplate_2��.R�� 
Grid_2��$��0��.+��� Border_2��
���   �$��-7,-2,-5.5,-2q�$��0.5,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_2��$
 Left=�$��0,0,0,0q����� *
Event_Type+�� DataTemplate_3��.R�� 
Grid_3��$��0��.+��� Border_3��
���   �$��-7,-2,-5.5,-2q�$��0.5,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_3��$
 Left=�$��0,0,0,0q����� *	Details+.��� -    �Name$ 
LayoutRoot��
LayoutRoot��  LoginBGBrush#���  $ 0.5,0.5&�$	��822��$��
-8,0,0,-49q�	 � RowDefinitions	 �� $
��637* ��� $	��26* ��� $	��23* �z�V� TransformGroup_1��.~��� ScaleTransform_1��
 �� ScaleX$
 1Q� �� ScaleY$ 1Q��� SkewTransform_1�� �� AngleX$ 0Q� �� AngleY$ 0Q��� RotateTransform_1�� �� Angle$ 0Q�T� TranslateTransform_1�� T� X$ 0Q� T� Y$ 0Q�.+��� Border_4�� �� CornerRadius$ 5,5,5,5�����	#19CDDD98$��0,-4,0,4q�$��3��� -   $ 	pnlHeader��	pnlHeader��$	��Top=�$��53��$��6,0,0,0q�.+�F� 
Path_1�� �� Stretch$
 Fill=�����p �B\�����@Q@�-\�����@S�%J-\�����@�!s-����Y�@�!s-\�����@Q�!s-�z�G#�@S�!s-�����&�@Xa�-	��%�@�-�z�G)�@Qf���z�G)�@S���
h"l�&�@t�����h�(�@���:��H#�@Q�-�\�����@S�-�����Y�@�)\�����@ �B\�����@  MsgBoxTitleColor#���   �� Left$ 0��
 �� Top$ 0��$��-6,0,0,0q��� -    ��Name$ tbHeader��tbHeader�� 2 .CLiquidationDetails_ProfitShare_xaml_tblHeader#���  $��
8,15,281,6q�$��NoWrap=�
�������$��Segoe UI)�$��16(�$y�Visible=�� -   $��grid1��	grid1��$��11,12,70,2.313q�.+��� -   $ tblDateCollected��tblDateCollected�� , (CLiquidationDetails_xaml_tbDateCollected#���  $��	20,59,0,0q�$��NoWrap=�
�������$��Segoe UI)�$��14(�$y�Visible=�$��38��$	��Top=�$
��Left=�$	��250���� -   $��txtDateCollected��txtDateCollected��  TextBoxGradient#���  {�.    TextBoxOutline#���  $��292,54,258,0q� 	 TextBoxStyle1#��� 	  �� TextWrapping$
 Wrap=�$��37��$	��Top=� �� 	MaxLength$ 20��$��14(�$
��Bold"� �� TextAlignment$
 Left=�$��Tahoma)�$��0q� �� 
IsReadOnly .  �� -	   $ tblRetailername��tblRetailerName�� ,
 (CLiquidationDetails_xaml_tblRetailerName#��� 
 $��	19,99,0,0q�$��NoWrap=�
�������$��Segoe UI)�$��14(�$y�Visible=�$��38��$	��Top=�$
��Left=�$	��250���� -
   $��txtRetailerName��txtRetailerName��#���  {�.  #���  $��292,97,258,0q�#��� 	 $
 Wrap=�$��37��$	��Top=�$ 20��$��14(�$
��Bold"�$
 Left=�$��Tahoma)�$��0q� .  �� -   $ tbGross��tblGross�� 0 ,CLiquidationDetailsProfitShare_xaml_tblGross#���  $��
19,153,0,0q�$��NoWrap=�
�������$��Segoe UI)�$��14(�$y�Visible=�$
��Left=�$	��250��$��39��$	��Top=��� -   $��txtGross��txtGross��#���  {�.  #���  $��Tahoma)�$��14(�$
��Bold"�$��37��$��293,147,257,0q�$ 20��#��� 	 $
 Left=�$
 Wrap=�$	��Top=�$��0q� .  �� -   $ tblTicketsExpected��$��Segoe UI)�$��14(�
�������$��39��$
��Left=�$��
17,199,0,0q� : 6CLiquidationDetailsProfitShare_xaml_tblTicketsExpected#���  $��NoWrap=�$	��Top=�$y�Visible=�$	��250���� -   $��txtTicketsExpected��txtTicketsExpected��#���  {�.  #���  $��Tahoma)�$��14(�$
��Bold"�$��37��$��293,191,257,0q�$ 20��#��� 	 $
 Left=�$
 Wrap=�$	��Top=�$��0q� .  �� -   $ tblNet��$��Segoe UI)�$��14(�
�������$��39��$
��Left=�$��
19,233,0,0q� # CLiquidationDetails_xaml_tblNet#���  $��NoWrap=�$	��Top=�$y�Visible=�$	��250���� -   $��txtNet��
txtNet��#���  {�.  #���  $��Tahoma)�$��14(�$
��Bold"� .  $��292,233,258,0q�$ 20��#��� 	 $
 Left=�$
 Wrap=�$��37��$	��Top=�$��0q��� -   $ tblNetValue��$��Segoe UI)�$��14(�
�������$
��Left=�$��19,277,0,306.687q� ( $CLiquidationDetails_xaml_tblNetValue#���  $��NoWrap=�$y�Visible=�$	��250���� -   $��txtNetValue��txtNetValue��#���  {�.  #���  $��Tahoma)�$��14(�$
��Bold"�$��292,275,258,314.687q�$ 20��#��� 	 $
 Left=�$
 Wrap=�$��0q� .  �� -   $ tblRetailerNegnet��tblRetailerNegnet��$
��Wrap=� . *CLiquidationDetails_xaml_tblRetailerNegNet#���  $��20,0,0,269.687q�
�������$��Segoe UI)�$��14(�$y�Visible=�$
��Left=�$	��250��$��41��$��Bottom=��� -    ��Name$ txtRetailernegNet��txtRetailernegNet��{�.  #���  #���  $��Tahoma)�$��14(�$
��Bold"�$��37�� .  $��293,0,257,272.687q�$ 20��#��� 	 $
 Left=�$
 Wrap=�$��Bottom=�$��0q��� -   $ tblRetailerShare��$��Segoe UI)�$��14(�
�������$��40��$
��Left=�$��19,0,0,227.687q� - )CLiquidationDetails_xaml_tblRetailerShare#���  $��NoWrap=�$��Bottom=�$y�Visible=�$	��250���� -   $ txtRetailerShare��txtRetailerShare��{�.  #���  #���  $��Tahoma)�$��14(�$
��Bold"�$��37�� .  $��293,0,257,230.687q�$ 20��#��� 	 $
 Left=�$
 Wrap=�$��Bottom=�$��0q��� -   $ tblTicketsPaid��$��Segoe UI)�$��14(�
�������$��39��$
��Left=�$��20,0,0,183.687q� + 'CLiquidationDetails_xaml_tblTicketspaid#���  $��NoWrap=�$��Bottom=�$y�Visible=�$	��250���� -   $ txtTicketsPaid��txtTicketspaid��{�.  #���  #���  $��Tahoma)�$��14(�$
��Bold"�$��37�� .  $��293,0,257,188.687q�$ 20��#��� 	 $
 Left=�$
 Wrap=�$��Bottom=�$��0q��� -   $ tblAdvanceToRetailer��$��Segoe UI)�$��14(�
�������$��39��$
��Left=�$��20,0,0,101.687q� 1 -CLiquidationDetails_xaml_tbladvancetoretailer#���  $��NoWrap=�$��Bottom=�$y�Visible=�$	��250���� -   $ txtAdvanceToRetailer��txtAdvanceToRetailer��#���  #���  $��Tahoma)�$��14(�$
��Bold"�$��37�� .  $��293,0,257,103.687q�$ 18��#��� 	 $
 Left=�$
 Wrap=�$��Bottom=�$��0q��� -   $( "tblRetailerSharebeforeFixedExpense��$��Segoe UI)�$��14(�
�������$��39��$
��Left=�$��20,0,0,143.687q� ? ;CLiquidationDetails_xaml_tblRetailerShareBeforeFixedExpense#���  $��NoWrap=�$��Bottom=�$y�Visible=�$	��250���� -   $( "txtRetailerSharebeforeFixedExpense��&"txtRetailerSharebeforeFixedExpense��{�.  #���  #���  $��Tahoma)�$��14(�$
��Bold"�$��37�� . $��293,0,257,145.687q�$ 20��#��� 	 $
 Left=�$
 Wrap=�$��Bottom=�$��0q��� -   $  tblFixedExpenseShareAmount��$��Segoe UI)�$��14(�
�������$��39��$
��Left=�$��20,0,0,59.687q� , (CLiquidationDetails_xaml_tblFixedExpense#���  $��NoWrap=�$��Bottom=�$y�Visible=�$	��250���� -   $  txtFixedExpenseShareAmount��txtFixedExpenseShareAmount��{�.  #���  #���  $��Tahoma)�$��14(�$
��Bold"�$��37�� . $��293,0,257,61.687q�$ 20��#��� 	 $
 Left=�$
 Wrap=�$��Bottom=�$��0q��� -   $ tblRetailer��$��Segoe UI)�$��14(�
�������$��39��$
��Left=�$��20,0,0,16.687q� ( $CLiquidationDetails_xaml_tblRetailer#���  $��NoWrap=�$��Bottom=�$y�Visible=�$	��150���� -    $ txtRetailer��txtRetailer��{�.  #���  #���  $��Tahoma)�$��14(�$
��Bold"�$��37�� . $��108,0,0,18.687q�$ 20��#��� 	 $
 Left=�$
 Wrap=�$��Bottom=�$
��Left=�$	��204���� -!   $ tblBalanceDue��$��Segoe UI)�$��14(�
�������$��39��$��335,0,309,15.687q� * &CLiquidationDetails_xaml_tblBalanceDue#���  $��NoWrap=�$��Bottom=�$y�Visible=��� -"   $ txtBalanceDue��txtBalanceDue��{�.  #���  #���  $��Tahoma)�$��14(�$
��Bold"�$��37�� . $��0,0,109,20.687q�$ 20��#��� 	 $
 Left=�$
 Wrap=�$��Bottom=�$��Right=�$	��192���� -#   $��
btnConfirm��
btnConfirm��$��Segoe UI)�$��14(�$��55�� * &CLiquidationDetails_PS_xaml_btnConfirm#���    
BMC_Button#���  $��0,0,111,-42.313q�$��Bottom=�$��Right=�$	��100���� -$   $��btnProfitShare��btnProfitShare��$��Segoe UI)�$��14(�$��55�� . *CLiquidationDetails_PS_xaml_btnProfitShare#���  #���  $��0,0,216,-42.313q�$��Bottom=�$��Right=�$	��100���� -%   $��	btnCancel��btnProfitShare��$��Segoe UI)�$��14(�$��55�� ( $CLiquidationDetails_PS_xaml_btnClose#���  #���  $��0,0,6,-42.313q�$��Bottom=�$��Right=�$	��100��F� 
Path_1��$
 Fill=�������}?�3-i��JK�3-i�`$SK�3-i0ZK`4i0ZK�u<i�0ZK�u<i�0ZK@�Di`$SK�Ki�JK�Kiѐ�}?�Ki�q=
ף��@�KiP�n?@�DiP�n?�u<i�P�n?�u<i�P�n?`4iq=
ף��@�3-i��}?�3-i$
��Left=�$	��Top=�$��199.975��$��1.998��$��	20,-4,0,0q����� LinearGradientBrush_1�� �� 
StartPoint$ -1.22086e-006,0.499939&� �� EndPoint$ 0.999997,0.499939&�.��� GradientStop_1�� � Color$ 	#FFC4CFBE�� � Offset$ 0Q�� GradientStop_2��$ 	#00C4CFBE��$ 1Q�