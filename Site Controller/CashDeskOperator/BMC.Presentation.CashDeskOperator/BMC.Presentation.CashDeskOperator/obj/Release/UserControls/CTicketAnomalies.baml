   M S B A M L   `   `   `  ����   BMC.Presentation.POSD    >BMC.Presentation.CashDeskManager.UserControls.CTicketAnomalies   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     �Name$  Window��$	��768��$
��1024�� � AllowsTransparency .  � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=���Z� .��>� H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -   UserControl_1��$	��855��$	��677�� J� RenderTransformOrigin$ 0.5,0.5&� � 	Resources    Date
 � Tag �� TextAlignment  StockNo  	Reference 
 Amount  Details��%�  &	        &	 �     &	 �    &	 �    &	 �    �� DataTemplate_1��.R�� 
Grid_1��$��10��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $��	PrintDate��	PrintDate��$ Date / Time��$
 Left=����� *	PrintDate+�� DataTemplate_2��.R�� 
Grid_2��$��10��.+��� Border_2��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $
��SEGM��SEGM��$ Stock No��$��0,0,3,0q�$ Center=����� *SEGM+�� DataTemplate_3��.R�� 
Grid_3��$��10��.+��� Border_3��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $��	Reference��	Reference��$ 	Reference��$��0,0,3,0q�$ Center=����� *	Reference+�� DataTemplate_4��.R�� 
Grid_4��$��10��.+��� Border_4��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $��Value��	Value��$ Amount��$ Right=����� *Value+�� DataTemplate_5��.R�� 
Grid_5��$��10��.+��� Border_5��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $��Details��Details��$ Details��$
 Left=����� *	Details+z�V� TransformGroup_1��.~��� ScaleTransform_1�� �� ScaleX$ 1Q�	 �� ScaleY$	 1Q��� SkewTransform_1��
 �� AngleX$
 0Q� �� AngleY$ 0Q��� RotateTransform_1�� �� Angle$ 0Q�T� TranslateTransform_1�� T� X$ 0Q� T� Y$ 0Q� � Triggers .��� 
Grid_6��$	��653��$	��834��  LoginBGBrush#���  .+�� -    �Name$ 	pnlHeader��	pnlHeader��$	��Top=�$��53�� �� Left$ 14.267��
 �� Top$ 0��$��6,0,0,0q�.+�F� 
Path_1�� �� Stretch$
 Fill=�����p �B\�����@Q@�-\�����@S�%J-\�����@�!s-����Y�@�!s-\�����@Q�!s-�z�G#�@S�!s-�����&�@Xa�-	��%�@�-�z�G)�@Qf���z�G)�@S���
h"l�&�@t�����h�(�@���:��H#�@Q�-�\�����@S�-�����Y�@�)\�����@ �B\�����@  MsgBoxTitleColor#���  $ 0��$ 0��$��	-6,0,18,0q��� -    ��Name$ btnExit��btnExit��  BMC_App_Exit#���  $ 0.455,0.272&�$��50��$	 750��$ 0��$��Right=�$��0,6,0,0q��� -    ��Name$ tbHeader��tbHeader�� " CTicketAnomalies_xaml_tbHeader#���  $��
8,15,281,6q�$��NoWrap=�
�������$��Segoe UI)�$��16(�$y�Visible=��� -    ��Name$ lvTicketAnomalies��lvTicketAnomalies��$ �� IsSynchronizedWithCurrentItem$
 TrueY� 	 Bmc_ListViewStyleFixedLock#��� 	 $��13(�$��Segoe UI)� "
 BMC_ListView_ListViewItemStyle#��� 
 $��
7,54,12,77q�$��Visible=�$��Visible=���.  �� View �� 
GridView_1��.P��� GridViewColumn_1�� * &CTicketAnomalies_xaml_GridViewColumn_1#���   �� Width$	 123�� �� CellTemplate# [ �� GridViewColumn_2�� * &CTicketAnomalies_xaml_GridViewColumn_2#���  $	 170��# [ �� GridViewColumn_3�� * &CTicketAnomalies_xaml_GridViewColumn_3#���  $	 153��# [ �� GridViewColumn_4�� * &CTicketAnomalies_xaml_GridViewColumn_4#���  $	 163��# [  �� GridViewColumn_5�� * &CTicketAnomalies_xaml_GridViewColumn_5#���  $	 203��# [ � -   $��prgTicketAnomalies��prgTicketAnomalies��$��25�� � Value$ 0Q�$��Bottom=�$��	9,0,11,51q��� -   $ btnPrint��btnPrint��$��79��$��41�� " CTicketAnomalies_xaml_btnPrint#���    Control_ViewDetailsStyle#���  $��14(�$��Segoe UI)�$
��Bold"� � Cursor$
 Hand��  
ListBorder#���  $��2��$ 6��$	 134��$��Right=�$��0,0,95,6q�$��Bottom=��� -	   $ 	btnExport��	btnExport��$y�Hidden=�$��79��$��41�� # CTicketAnomalies_xaml_btnExport#���  #���  $��14(�$��Segoe UI)�$
��Bold"�$
 Hand��#���  $��2��$ 6��$	 134��$��Right=�$��0,0,11,6q�$��Bottom=�