   M S B A M L   `   `   `  ����   BMC.Presentation.POS?    9BMC.Presentation.CashDeskManager.UserControls.CExceptions   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     �Name$  Window��$	��768��$
��1024�� � AllowsTransparency .  � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=���Z� .��>� H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -   UserControl_1��$	��855��$	��677�� J� RenderTransformOrigin$ 0.5,0.5&�$��20,0,0,0q� � 	Resources    Type
 � Tag �� TextAlignment  Position  Date 
 Ticket 	 Value 	 Asset  CreateCompleted��%�  &	        &	 �     &	 �    &	 �    &	 �    &	 d    &	 =    �� DataTemplate_1��.R�� 
Grid_1��$��10��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $
��Type��Type��$
 Type��$
 Left=����� *Type+�� DataTemplate_2��.R�� 
Grid_2��$��10��.+��� Border_2��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $��Position��Position��$ Position��$��0,0,3,0q�$
 Left=����� *
Position+�� DataTemplate_3��.R�� 
Grid_3��$��10��.+��� Border_3��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $��	PrintDate��	PrintDate��$
 Date��$
 Left=����� *	PrintDate+�� DataTemplate_4��.R�� 
Grid_4��$��10��.+��� Border_4��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $��Ticket��
Ticket��$ Ticket��$
 Left=����� *Ticket+�� DataTemplate_5��.R�� 
Grid_5��$��10��.+��� Border_5��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $��Value��	Value��$ Value��$ Right=����� *Amount+�� DataTemplate_6��.R�� 
Grid_6��$��10��.+��� Border_6��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $��Asset��	Asset��$ Asset��$
 Left=����� *Asset+�� DataTemplate_7��.R�� 
Grid_7��$��10��.+��� Border_7��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $��CreateCompleted��CreateCompleted��$ Create Completed��$
 Left=����� *CreateCompleted+z�V� TransformGroup_1��.~��� ScaleTransform_1�� �� ScaleX$ 1Q�	 �� ScaleY$	 1Q��� SkewTransform_1��
 �� AngleX$
 0Q� �� AngleY$ 0Q��� RotateTransform_1�� �� Angle$ 0Q�T� TranslateTransform_1�� T� X$ 0Q� T� Y$ 0Q� � Triggers .��� 
Grid_8��$	��653��$	��834��  LoginBGBrush#���  $��20,0,0,0q�.+�� -    �Name$ 	pnlHeader��	pnlHeader��$	��Top=�$��53�� �� Left$ 14.267��
 �� Top$ 0��$��1,0,19,0q�.+�F� 
Path_1�� �� Stretch$
 Fill=�����p �B\�����@Q@�-\�����@S�%J-\�����@�!s-����Y�@�!s-\�����@Q�!s-�z�G#�@S�!s-�����&�@Xa�-	��%�@�-�z�G)�@Qf���z�G)�@S���
h"l�&�@t�����h�(�@���:��H#�@Q�-�\�����@S�-�����Y�@�)\�����@ �B\�����@  MsgBoxTitleColor#���  $ 0��$ 0��$��-6,0,0,0q�$
��Left=�$	��780���� -    ��Name$ btnExit��btnExit�� 	 BMC_App_Exit#��� 	 $ 0.455,0.272&�$��50��$	 750��$ 0��$��Right=�$��0,6,-7,0q��� -    ��Name$ tbHeader��tbHeader�� 
 CExceptions_xaml_tbHeader#��� 
 $��
8,15,281,6q�$��NoWrap=�
�������$��Segoe UI)�$��16(�$y�Visible=��� -   $ 	btnExport��	btnExport��$��79��$��41��$y�Hidden=� � Cursor$
 Hand��  Control_ViewDetailsStyle#���  $��2��$ 6��$	 134��$��Right=�$��	0,0,116,7q�$��Bottom=�.���� TextBlock_1��   CExceptions_xaml_TextBlock_1#���  $
��Wrap=�$��14(�$��Segoe UI)�$
��Bold"��� -   $��btnActivate��btnActivate��$ 6��$	 134��$
 Hand��$��41��$��Right=�$��	0,0,207,6q�#���  $��2��$��Bottom=�$��79��$y�Hidden=�.���� TextBlock_2��$��Segoe UI)�$��14(�$
��Bold"�   CExceptions_xaml_TextBlock_2#���  $
��Wrap=��� -    ��Name$ lvExceptions��lvExceptions��$ �� IsSynchronizedWithCurrentItem$
 TrueY�$��13(�$��Segoe UI)�  Bmc_ListViewStyleFixedLock#���   " BMC_ListView_ListViewItemStyle#���  $��
0,53,12,77q�$��Visible=�$
��Auto=���.  �� View �� 
GridView_1��.P��� GridViewColumn_1�� % !CExceptions_xaml_GridViewColumn_1#���   �� Width$ 85�� �� CellTemplate# [  �� GridViewColumn_2�� % !CExceptions_xaml_GridViewColumn_2#���  $ 90��# [ �� GridViewColumn_3�� % !CExceptions_xaml_GridViewColumn_3#���  $	 180��# [ �� GridViewColumn_4�� % !CExceptions_xaml_GridViewColumn_4#���  $	 173��# [ �� GridViewColumn_5�� % !CExceptions_xaml_GridViewColumn_5#���  $	 113��# [ �� GridViewColumn_6�� % !CExceptions_xaml_GridViewColumn_6#���  $	 113��# [ �� GridViewColumn_7�� % !CExceptions_xaml_GridViewColumn_7#���  $	 160��# [ � -	   $��prgExceptions��prgExceptions��$��25�� � Value$ 0Q�$��Bottom=�$��
11,0,13,48q�