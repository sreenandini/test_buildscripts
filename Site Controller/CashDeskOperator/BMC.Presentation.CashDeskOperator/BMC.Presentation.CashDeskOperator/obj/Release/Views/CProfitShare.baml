   M S B A M L   `   `   `  ����   BMC.Presentation.POS#    BMC.Presentation.CProfitShare   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     �Name$  Window��$	��389��$	��543��   MainScreenBG#���    � AllowsTransparency .  � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � ShowInTaskbar .  .��� -    �Name$ 
LayoutRoot��
LayoutRoot��$	��515��$��14,17,14,10q�{�.  � RowDefinitions �� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��50 ��� $��* � � ColumnDefinitions �� $��125.752 ��� $��60 ��� $
��260* �.+�� -   $ 	pnlHeader��	pnlHeader�� �� Left$ 14.267��
	 �� Top$	 0��$��
0,-15,0,35q�$��4��$��2��.+�F� 
Path_1��
 �� Stretch$

 Fill=�����p �B\�����@Q@�-\�����@S�%J-\�����@�!s-����Y�@�!s-\�����@Q�!s-�z�G#�@S�!s-�����&�@Xa�-	��%�@�-�z�G)�@Qf���z�G)�@S���
h"l�&�@t�����h�(�@���:��H#�@Q�-�\�����@S�-�����Y�@�)\�����@ �B\�����@  MsgBoxTitleColor#���  $ 0��$	 0��$��-15,-9,-19,25q��� txtblkProfitShare�� �� TextAlignment$
 Left=�$��Segoe UI)�$
��Bold"�$��16(�$	��Top=�$
��Left=�$��20,0,0,0q� J� RenderTransformOrigin$ 0.5,0.5&� ' #CProfitShare_xaml_txtblkProfitShare#���  ���	#FFFFFFFFz�V� TransformGroup_3��.~��� ScaleTransform_3�� �� ScaleX$ 1Q� �� ScaleY$ 1Q��� SkewTransform_3�� �� AngleX$ 0Q� �� AngleY$ 0Q��� RotateTransform_3�� �� Angle$ 0Q�T� TranslateTransform_3�� T� X$ 0Q� T� Y$ 0Q�F� 
Path_1��$

 Fill=�������}?�3-i��JK�3-i�`$SK�3-i0ZK`4i0ZK�u<i�0ZK�u<i�0ZK@�Di`$SK�Ki�JK�Kiѐ�}?�Ki�q=
ף��@�KiP�n?@�DiP�n?�u<i�P�n?�u<i�P�n?`4iq=
ף��@�3-i��}?�3-i$��22,18,233,20q�$��3������ LinearGradientBrush_1�� �� 
StartPoint$ -1.22086e-006,0.499939&� �� EndPoint$ 0.999997,0.499939&�.��� GradientStop_1�� � Color$ 	#FFC4CFBE�� � Offset$ 0Q�� GradientStop_2��$ 	#00C4CFBE��$ 1Q��� -    ��Name$ txtblkProfitShareGroup��$��1��txtblkProfitShareGroup�� , (CProfitShare_xaml_txtblkProfitShareGroup#���  $��20,9,0,2q�$��NoWrap=�
�������$��Segoe UI)�$��14(�$y�Visible=�$��3��$
��Left=�$��235.752���� -    ��Name$ cboProfitShareGroup��cboProfitShareGroup��$ �� IsSynchronizedWithCurrentItem$
 TrueY�  BMC_ComboBoxStyle#���  $��	26,0,28,0q�$��2��$��1��
���   �$��14(� �� DisplayMemberPath$ ProfitShareGroup_Name�� �� SelectedValuePath$ ProfitShareGroup_ID��$��35���� -   $ txtblkExpenseShareGroup��$��2��txtblkExpenseShareGroup�� - )CProfitShare_xaml_txtblkExpenseShareGroup#���  $��20,9,0,2q�$��NoWrap=�
�������$��Segoe UI)�$��14(�$y�Visible=�$��3��$
��Left=�$��235.752���� -   $ cboExpenseShareGroup��cboExpenseShareGroup��$��	26,0,28,0q�$
 TrueY�#���  $��2��$��2��
���   �$��14(�$��35��$��Bottom=��� -   $ txtblkExpenseShareAmount��$��3��txtblkExpenseShareAmount�� . *CProfitShare_xaml_txtblkExpenseShareAmount#���  $��	20,10,0,1q�$��NoWrap=�
�������$��Segoe UI)�$��14(�$y�Visible=�$��3��$
��Left=�$��235.752���� -	   $��txtExpenseShareAmount��$��3��$��2��txtExpenseShareAmount��$��Center=� �� TextWrapping$ NoWrap=�$��14(�  TextBoxStyle1#���  $y�Visible=� �� 	MaxLength$ 13��  TextBoxGradient#���   	 TextBoxOutline#��� 	  
 	SegoeUI20#��� 
 {�. $
��Left=�$��26,5.716,28,0q��� -
   $ txtblkWriteOffExpense��$��4��txtblkWriteOffExpense�� + 'CProfitShare_xaml_txtblkWriteOffExpense#���  $��20,9,0,2q�$��NoWrap=�
�������$��Segoe UI)�$��14(�$y�Visible=�$��3��$
��Left=�$��235.752���� -   $��txtWriteOffExpense��$��4��$��2��txtWriteOffExpense��$�� ��$ NoWrap=�$��14(�#���  #���  #��� 	 $ 13��#��� 
 {�. $
��Left=�$��Center=�$��	26,2,28,3q�$y�Visible=��� -   $  txtblkCarriedForwareAmount��$��5��txtblkCarriedForwareAmount�� 0 ,CProfitShare_xaml_txtblkCarriedForwareAmount#���  $��20,9,0,2q�$��NoWrap=�
�������$��Segoe UI)�$��14(�$y�Visible=�$��3��$
��Left=�$��235.752���� -   $��txtCarriedForwardAmount��$��5��$��2��txtCarriedForwardAmount��$�� ��$ NoWrap=�$��14(�#���  #���  #��� 	 #��� 
 {�.  $
��Left=�$��Center=�$��	26,2,28,3q�$y�Visible=��� -   $ txtblkCalendarPeriod��$��6��txtblkCalendarPeriod�� * &CProfitShare_xaml_txtblkCalendarPeriod#���  $��20,5,0,6q�$��NoWrap=�
�������$��Segoe UI)�$��14(�$y�Visible=�$��3��$
��Left=�$��235.752���� -   $��txtblkPayPeriod��$��6��txtblkPayPeriod��$��Segoe UI)�$��14(�
�������$��	26,5,28,0q�$
��Wrap=�$y�Visible=�$��2���� -   $��7��$��1��	btnOk��$
��Left=�  
BMC_Button#���    CProfitShare_xaml_btnOk#���  $y�Visible=�$��	18,11,0,5q�$��2���� -   $��7��$��2��	btnCancel��#���    CProfitShare_xaml_btnCancel#���  $y�Visible=�$��70,11,158,5q�