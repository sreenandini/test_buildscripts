   M S B A M L   `   `   `  ����   BMC.Presentation.POS:    4BMC.Presentation.CashDeskManager.UserControls.CDebug   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -   UserControl_1��$	��736��$	��520��  J� RenderTransformOrigin$  0.5,0.5&� � 	Resources  
  Ticket �� TextAlignment  	PrintDate  PrintDevice 
 Amount  PayDate  	PayDevice 
 Status��%�  &	        &	 �     &	 �    &	 h    &	 0    &	 �    &	 �    �� DataTemplate_1��.R�� 
Grid_1��$��10��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_1��$
 Left=����� *Ticket+�� DataTemplate_2��.R�� 
Grid_2��$��10��.+��� Border_2��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_2��$��0,0,3,0q�$
 Left=����� *	PrintDate+�� DataTemplate_3��.R�� 
Grid_3��$��10��.+��� Border_3��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_3��$
 Left=����� *Asset+�� DataTemplate_4��.R�� 
Grid_4��$��10��.+��� Border_4��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_4��$ Right=����� *Value+�� DataTemplate_5��.R�� 
Grid_5��$��10��.+��� Border_5��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_5��$ Right=����� *	PayDate+�� DataTemplate_6��.R�� 
Grid_6��$��10��.+��� Border_6��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_6��$
 Left=����� *	PayDevice+�� DataTemplate_7��.R�� 
Grid_7��$��10��.+��� Border_7��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_7��$
 Left=����� *Status+z�V� TransformGroup_1��.~��� ScaleTransform_1�� �� ScaleX$ 1Q� �� ScaleY$ 1Q��� SkewTransform_1�� �� AngleX$ 0Q� �� AngleY$ 0Q��� RotateTransform_1�� �� Angle$ 0Q�T� TranslateTransform_1�� T� X$ 0Q�	 T� Y$	 0Q�
 � Triggers
 .��� 
Grid_8��$	��510��$	��730��  LoginBGBrush#���  .+�� -    �Name$ 	pnlHeader��	pnlHeader��$	��Top=�$��53�� �� Left$ 14.267��
 �� Top$ 0��$��6,0,0,0q�.+�F� 
Path_1�� �� Stretch$
 Fill=�����p �B\�����@Q@�-\�����@S�%J-\�����@�!s-����Y�@�!s-\�����@Q�!s-�z�G#�@S�!s-�����&�@Xa�-	��%�@�-�z�G)�@Qf���z�G)�@S���
h"l�&�@t�����h�(�@���:��H#�@Q�-�\�����@S�-�����Y�@�)\�����@ �B\�����@  MsgBoxTitleColor#���  $ 0��$ 0��$��	-6,0,18,0q��� -    ��Name$ btnExit��btnExit�� 	 BMC_App_Exit#��� 	 $  0.455,0.272&�$��50��$	 750��$ 0��$��Right=�$��0,6,0,0q��� -    ��Name$ tbHeader��tbHeader�� 
 CDebug_xaml_tbHeader#��� 
 $��
8,15,281,6q�$��NoWrap=�
�������$��Segoe UI)�$��16(�$y�Visible=��� -    ��Name$ lvVoucherStatus��lvVoucherStatus��$ �� IsSynchronizedWithCurrentItem$
 TrueY�  Bmc_ListViewStyleFixedLock#���  $��13(�$��Segoe UI)� " BMC_ListView_ListViewItemStyle#���  $��18,54,18,78q�$��Visible=�$��Visible=���.  �� View �� 
GridView_1��.P��� GridViewColumn_1��   CDebug_xaml_GridViewColumn_1#���   �� Width$	 193�� �� CellTemplate# [  �� GridViewColumn_2��   CDebug_xaml_GridViewColumn_2#���  $	 120��# [ �� GridViewColumn_3��   CDebug_xaml_GridViewColumn_3#���  $ 90��# [ �� GridViewColumn_4��   CDebug_xaml_GridViewColumn_4#���  $	 153��# [ �� GridViewColumn_5��   CDebug_xaml_GridViewColumn_5#���  $	 143��# [ �� GridViewColumn_6��   CDebug_xaml_GridViewColumn_6#���  $	 113��# [ �� GridViewColumn_7��   CDebug_xaml_GridViewColumn_7#���  $ 60��# [ � -   $��prgDebugState��prgDebugState��$��25�� � Value$ 0Q�$��Bottom=�$��
14,0,10,51q��� -   $ btnClose��btnClose��$��79��$��41��$y�Hidden=� � Cursor$
 Hand��  Control_ViewDetailsStyle#���  $��2��$ 6��$	 134��$��Right=�$��0,0,28,9q�$��Bottom=�.���� TextBlock_8��  CDebug_xaml_TextBlock_8#���  $
��Wrap=�$��14(�$��Segoe UI)�$
��Bold"�