   M S B A M L   `   `   `  ����   BMC.Presentation.POS'    !BMC.Presentation.CReadLiquidation   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     �Name$  Window��$	��675��$	��633��   MainScreenBG#���    � AllowsTransparency .  � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � ShowInTaskbar .  .��� -    �Name$ 
LayoutRoot��
LayoutRoot��$��	11,39,0,0q� � RowDefinitions �� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��* � � ColumnDefinitions �� $��125.752 ��� $	��120 ��� $	��260 ��� $	��100 �.+�� -   $ 	pnlHeader��	pnlHeader�� �� Left$ 14.267��
	 �� Top$	 0��$��
0,-15,0,35q�$��4��$��2��.+�F� 
Path_1��
 �� Stretch$

 Fill=�����p �B\�����@Q@�-\�����@S�%J-\�����@�!s-����Y�@�!s-\�����@Q�!s-�z�G#�@S�!s-�����&�@Xa�-	��%�@�-�z�G)�@Qf���z�G)�@S���
h"l�&�@t�����h�(�@���:��H#�@Q�-�\�����@S�-�����Y�@�)\�����@ �B\�����@  MsgBoxTitleColor#���  $ 0��$	 0��$��-12,-29,-12,24q��� txtblkReadBasedLiq�� �� TextAlignment$
 Left=�$��Segoe UI)�$
��Bold"�$��16(�$	��Top=�$
��Left=�$��10,0,0,0q� J� RenderTransformOrigin$ 0.5,0.5&� 2 .CReadLiquidation_xaml_txtblkLiquidationDetails#���  ���	#FFFFFFFFz�V� TransformGroup_3��.~��� ScaleTransform_3�� �� ScaleX$ 1Q� �� ScaleY$ 1Q��� SkewTransform_3�� �� AngleX$ 0Q� �� AngleY$ 0Q��� RotateTransform_3�� �� Angle$ 0Q�T� TranslateTransform_3�� T� X$ 0Q� T� Y$ 0Q�F� 
Path_1��$

 Fill=�������}?�3-i��JK�3-i�`$SK�3-i0ZK`4i0ZK�u<i�0ZK�u<i�0ZK@�Di`$SK�Ki�JK�Kiѐ�}?�Ki�q=
ף��@�KiP�n?@�DiP�n?�u<i�P�n?�u<i�P�n?`4iq=
ף��@�3-i��}?�3-i$��10,10,233,28q�$��3������ LinearGradientBrush_1�� �� 
StartPoint$ -1.22086e-006,0.499939&� �� EndPoint$ 0.999997,0.499939&�.��� GradientStop_1�� � Color$ 	#FFC4CFBE�� � Offset$ 0Q�� GradientStop_2��$ 	#00C4CFBE��$ 1Q��� -    ��Name$ 
txtblkDate��$��1��
txtblkDate�� $  CReadLiquidation_xaml_txtblkDate#���  $��5,5,0,6q�$��NoWrap=�
�������$��Segoe UI)�$��14(�$y�Visible=�$��2��$
��Left=�$��235.752���� -   $��txtDate��$��1��$��2��txtDate��  TextBoxGradient#���  {�.    TextBoxOutline#���  $��75.03,3,71,0q�  TextBoxStyle1#���   �� TextWrapping$
 Wrap=� �� 	MaxLength$ 20��$��14(�$
��Bold"� �� TextAlignment$
 Left=�$��Tahoma)�$��0q� �� 
IsReadOnly .  $��2���� -   $ txtblkRetailername��$��2��txtblkRetailerName�� , (CReadLiquidation_xaml_txtblkRetailerName#���  $��5,5,0,6q�$��NoWrap=�
�������$��Segoe UI)�$��14(�$y�Visible=�$��2���� -   $��txtRetailerName��$��2��$��2��txtRetailerName��#���  {�.  #���  $��75.03,3,71,0q�#���  $
 Wrap=�$ 20��$��14(�$
��Bold"�$
 Left=�$��Tahoma)�$��0q� .  $��2���� -   $ tbGross��$��3��txtblkGross�� % !CReadLiquidation_xaml_txtblkGross#���  $��5,5,0,6q�$��NoWrap=�
�������$��Segoe UI)�$��14(�$y�Visible=�$��2���� -	   $��txtGross��$��3��$��2��txtGross��#���  {�.  #���  $��Tahoma)�$��14(�$
��Bold"�$��75.03,3,71,0q�$ 20��#���  $
 Left=�$
 Wrap=�$��0q� .  $��2���� -
   $ txtblkTicketsExpected��$��4��$��Segoe UI)�$��14(�
�������$��	5,5,191,6q� /	 +CReadLiquidation_xaml_txtblkTicketsExpected#��� 	 $
��Wrap=�$y�Visible=�$��3���� -   $��txtTicketsExpected��$��4��$��2��txtTicketsExpected��#���  {�.  #���  $��Tahoma)�$��14(�$
��Bold"�$��75.03,3,71,0q�$ 20��#���  $
 Left=�$
 Wrap=�$��0q� .  $��2���� -   $ 	txtblkNet��$��5��$��Segoe UI)�$��14(�
�������$��	5,5,191,6q� #
 CReadLiquidation_xaml_txtblkNet#��� 
 $
��Wrap=�$y�Visible=�$��3���� -   $��txtNet��$��5��$��2��
txtNet��#���  {�.  #���  $��Tahoma)�$��14(�$
��Bold"� .  $��75.03,3,71,0q�$ 20��#���  $
 Left=�$
 Wrap=�$��0q�$��2���� -   $ txtblkNetValue��$��6��$��Segoe UI)�$��14(�
�������$��	5,5,191,6q� ( $CReadLiquidation_xaml_txtblkNetValue#���  $
��Wrap=�$y�Visible=�$��3���� -   $��txtNetValue��$��6��$��2��txtNetValue��#���  {�.  #���  $��Tahoma)�$��14(�$
��Bold"�$��75.03,3,71,0q�$ 20��#���  $
 Left=�$
 Wrap=�$��0q� .  $��2���� -   $ txtblkRetailerNegnet��$��7��txtblkRetailerNegnet��$
��Wrap=� . *CReadLiquidation_xaml_txtblkRetailerNegNet#���  $��	5,5,191,6q�
�������$��Segoe UI)�$��14(�$y�Visible=�$��2��$��3���� -    ��Name$ txtRetailernegNet��$��7��$��2��txtRetailernegNet��{�.  #���  #���  $��Tahoma)�$��13(�$
��Bold"� .  $��75.03,2,71,1q�$ 20��#���  $
 Left=�$
 Wrap=�$��0q�$��2���� -   $ txtblkRetailerShare��$��8��$��Segoe UI)�$��14(�
�������$��5,5,0,39q� - )CReadLiquidation_xaml_txtblkRetailerShare#���  $��NoWrap=�$y�Visible=�$��2��$��2���� -   $ txtRetailerShare��$��8��$��2��txtRetailerShare��{�.  #���  #���  $��Tahoma)�$��14(�$
��Bold"� .  $��75.03,3,71,0q�$ 20��#���  $
 Left=�$
 Wrap=�$��0q�$��2���� -   $ txtblkTicketsPaid��$��9��$��Segoe UI)�$��14(�
�������$��5,5,0,6q� + 'CReadLiquidation_xaml_txtblkTicketspaid#���  $��NoWrap=�$y�Visible=�$��2���� -   $ txtTicketsPaid��$��9��$��2��txtTicketspaid��{�.  #���  #���  $��Tahoma)�$��14(�$
��Bold"� .  $��75.03,3,71,0q�$ 20��#���  $
 Left=�$
 Wrap=�$��0q�$��2���� -   $ txtblkAdvanceToRetailer��$��10��$��Segoe UI)�$��14(�
�������$��	5,5,191,6q� 1 -CReadLiquidation_xaml_txtblkAdvancetoRetailer#���  $��NoWrap=�$y�Visible=�$��3���� -   $ txtAdvanceToRetailer��$��10��$��2��txtAdvanceToRetailer��#���  #���  $��Tahoma)�$��14(�$
��Bold"� .  $��0,3,71,0q�$ 18��#���  $��Right=�$
 Left=�$
 Wrap=�$��0q�$��2��$	��216���� -   $* $txtblRetailerSharebeforeFixedExpense��$��11��$��Segoe UI)�$��14(�
�������$��	5,5,191,6q� ? ;CReadLiquidation_xaml_txtblkRetailerShareBeforeFixedExpense#���  $��NoWrap=�$y�Visible=�$��3���� -   $( "txtRetailerSharebeforeFixedExpense��$��11��$��2��&"txtRetailerSharebeforeFixedExpense��{�.  #���  #���  $��Tahoma)�$��14(�$
��Bold"�$��37�� . $��0,3,71,0q�$ 20��#���  $
 Left=�$
 Wrap=�$	��Top=�$��Right=�$��2��$	��216���� -   $ txtblkFixedExpense��$��12��$��Segoe UI)�$��14(�
�������$��5,5,0,6q� , (CReadLiquidation_xaml_txtblkFixedExpense#���  $��NoWrap=�$y�Visible=��� -   $ txtFixedExpense��$��12��$��2��txtFixedExpense��{�.  #���  #���  $��Tahoma)�$��14(�$
��Bold"�$��37�� . $��0,3,71,0q�$ 20��#���  $
 Left=�$
 Wrap=�$	��Top=�$��Right=�$��2��$	��216���� -   $ txtblkRetailer��$��13��$��Segoe UI)�$��14(�
�������$��	5,5,112,0q� ( $CReadLiquidation_xaml_txtblkRetailer#���  $��NoWrap=�$y�Visible=�$��2���� -   $ txtRetailer��$��13��$��1��txtRetailer��{�.  #���  #���  $��Tahoma)�$��14(�$
��Bold"� . $��14,3,0,0q�$ 20��#���  $
 Left=�$
 Wrap=�$��2��$
��Left=�$	��160���� -   $ txtblkBalanceDue��$��13��$��2��$��Segoe UI)�$��14(�
�������$��	65,5,71,1q� * &CReadLiquidation_xaml_txtblkBalanceDue#���  $��NoWrap=�$y�Visible=��� -   $ txtBalanceDue��$��13��$��2��txtBalanceDue��{�.  #���  #���  $��Tahoma)�$��14(�$
��Bold"�$��37�� . $��0,3,6.993,0q�$ 20��#���  $
 Left=�$
 Wrap=�$	��Top=�$��2��$��Right=�$	��163���� -    $��btnProfitShare��$��14��btnProfitShare��$��Segoe UI)�$��14(� ( $CReadLiquidation_xaml_btnProfitShare#���    
BMC_Button#���  $��95,11,165,4q�$��1��$��2���� -!   $��
btnConfirm��$��14��
btnConfirm��$��Segoe UI)�$��14(� $  CReadLiquidation_xaml_btnConfirm#���  #���  $��107,11,33,4q�$��2���� -"   $��btnClose��$��14��btnClose��$��Segoe UI)�$��14(� " CReadLiquidation_xaml_btnClose#���  #���  $��0,11,2,4q�$��2��$��2��$��Right=�$	��120��