   M S B A M L   `   `   `  ����   BMC.Presentation.POS>    8BMC.Presentation.CashDeskManager.UserControls.CLiability   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     �Name$  Window��$	��768��$
��1024�� � AllowsTransparency .  � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=���Z� .��>� H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -   UserControl_1��$	��844��$	��677�� J� RenderTransformOrigin$ 0.5,0.5&� � 	Resources    	PrintDate
 � Tag �� TextAlignment  PrintDevice 	 Asset  	ClaimDate  ClaimDevice 
 Amount 
 Ticket 
 Status��%�  &	        &	 �     &	 �    &	 �    &	 �    &	 �    &	 �    &	 y    �� DataTemplate_1��.R�� 
Grid_1��$��10��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $��	PrintDate��	PrintDate��$ 
Print Date��$
 Left=����� *	PrintDate+�� DataTemplate_2��.R�� 
Grid_2��$��10��.+��� Border_2��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $��Position��Position��$ Print Device��$��0,0,3,0q�$
 Left=����� *
Position+�� DataTemplate_3��.R�� 
Grid_3��$��10��.+��� Border_3��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $��Machine��Machine��$ 
Game Title��$��0,0,3,0q�$
 Left=����� *	Machine+�� DataTemplate_4��.R�� 
Grid_4��$��10��.+��� Border_4��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $��PayDate��PayDate��$ 
Claim Date��$��0,0,3,0q�$
 Left=����� *	PayDate+�� DataTemplate_5��.R�� 
Grid_5��$��10��.+��� Border_5��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $��	PayDevice��	PayDevice��$ Claim Device��$��0,0,3,0q�$
 Left=����� *	PayDevice+�� DataTemplate_6��.R�� 
Grid_6��$��10��.+��� Border_6��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $��Amount��
Amount��$ Value��$ Right=����� *Amount+�� DataTemplate_7��.R�� 
Grid_7��$��10��.+��� Border_7��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $��Ticket��
Ticket��$ Ticket��$
 Left=����� *Ticket+�� DataTemplate_8��.R�� 
Grid_8��$��10��.+��� Border_8��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $��Status��
Status��$ Status��$
 Left=����� *Status+z�V� TransformGroup_1��.~��� ScaleTransform_1�� �� ScaleX$ 1Q�	 �� ScaleY$	 1Q��� SkewTransform_1��
 �� AngleX$
 0Q� �� AngleY$ 0Q��� RotateTransform_1�� �� Angle$ 0Q�T� TranslateTransform_1�� T� X$ 0Q� T� Y$ 0Q� � Triggers .��� 
Grid_9��$	��653��$	��823��  LoginBGBrush#���  .+�� -    �Name$ 	pnlHeader��	pnlHeader��$	��Top=�$��53�� �� Left$ 14.267��
 �� Top$ 0��$��6,0,0,0q�.+�F� 
Path_1�� �� Stretch$
 Fill=�����p �B\�����@Q@�-\�����@S�%J-\�����@�!s-����Y�@�!s-\�����@Q�!s-�z�G#�@S�!s-�����&�@Xa�-	��%�@�-�z�G)�@Qf���z�G)�@S���
h"l�&�@t�����h�(�@���:��H#�@Q�-�\�����@S�-�����Y�@�)\�����@ �B\�����@ 	 MsgBoxTitleColor#��� 	 $ 0��$ 0��$��-6,0,0,0q�$
��Left=�$	��906���� -    ��Name$ btnExit��btnExit�� 
 BMC_App_Exit#��� 
 $ 0.455,0.272&�$��50��$	 750��$ 0��$��Right=�$��0,6,0,0q��� -    ��Name$ tbHeader��tbHeader��  CLiability_xaml_tbHeader#���  $��
8,15,281,6q�$��NoWrap=�
�������$��Segoe UI)�$��16(�$y�Visible=��� -    ��Name$ lvLiability��lvLiability��$ �� IsSynchronizedWithCurrentItem$
 TrueY�  Bmc_ListViewStyleFixedLock#���  $��13(�$��Segoe UI)� " BMC_ListView_ListViewItemStyle#���  $��-1,48,12,77q�$��Visible=�$
��Auto=���.  �� View �� 
GridView_1��.P��� GridViewColumn_1�� $  CLiability_xaml_GridViewColumn_1#���   �� Width$	 153�� �� CellTemplate# [  �� GridViewColumn_2�� $  CLiability_xaml_GridViewColumn_2#���  $	 140��# [ �� GridViewColumn_3�� $  CLiability_xaml_GridViewColumn_3#���  $	 130��# [ �� GridViewColumn_4�� $  CLiability_xaml_GridViewColumn_4#���  $	 170��# [ �� GridViewColumn_5�� $  CLiability_xaml_GridViewColumn_5#���  $ 0��# [ �� GridViewColumn_6�� $  CLiability_xaml_GridViewColumn_6#���  $	 100��# [ �� GridViewColumn_7�� $  CLiability_xaml_GridViewColumn_7#���  $	 163��# [ �� GridViewColumn_8�� $  CLiability_xaml_GridViewColumn_8#���  $	 320��# [ � -   $��prgLiability��prgLiability��$��25�� � Value$ 0Q�$��Bottom=�$��
11,0,12,50q��� -   $ 	btnExport��	btnExport��$y�Hidden=�$��79��$��41��  CLiability_xaml_btnExport#���    Control_ViewDetailsStyle#���  $��14(�$��Segoe UI)�$
��Bold"� � Cursor$
 Hand��  
ListBorder#���  $��2��$ 6��$	 134��$��Right=�$��0,0,12,7q�$��Bottom=�