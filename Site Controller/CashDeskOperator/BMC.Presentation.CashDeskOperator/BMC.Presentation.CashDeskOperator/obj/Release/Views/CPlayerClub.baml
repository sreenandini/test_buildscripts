   M S B A M L   `   `   `  ����   BMC.Presentation.POS"    BMC.Presentation.CPlayerClub   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     >�Name$  UserControl��UserControl��$	��870��$	��660�� J� RenderTransformOrigin$ 0.5,0.5&� � 	Resources  	  Prize �� TextAlignment  UnitPointValue  UnitCashValue��%�  &	        &	 �     &	 �    �� DataTemplate_1��.R�� 
Grid_1��$��10��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_1��$
 Left=����� *	PrizeName+�� DataTemplate_2��.R�� 
Grid_2��$��10��.+��� Border_2��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_2��$��0,0,3,0q�$ Right=����� *RedeemPoints+�� DataTemplate_3��.R�� 
Grid_3��$��10��.+��� Border_3��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_3��$ Right=����� *	AuthAward+z�V� TransformGroup_1��.~��� ScaleTransform_1�� �� ScaleX$ 1Q� �� ScaleY$ 1Q��� SkewTransform_1�� �� AngleX$ 0Q� �� AngleY$ 0Q��� RotateTransform_1�� �� Angle$ 0Q�T� TranslateTransform_1��	 T� X$	 0Q�
 T� Y$
 0Q� � Triggers .��� -    �Name$ 
LayoutRoot��
LayoutRoot��$ 0.5,0.5&���Z� z�V� TransformGroup_2��.~��� ScaleTransform_2��$ 1Q�$ 1Q��� SkewTransform_2��$ 0Q�$ 0Q��� RotateTransform_2��$ 0Q�T� TranslateTransform_2��$	 0Q�$
 0Q�.+��� Border_4��$��Stretch=�$��Stretch=�$
��Auto��$
��Auto�� �� CornerRadius$ 5,5,5,5�����	#19FFFFFF$��-1.335,0,1.335,0q�F� 
Path_1�� �� Stretch$
 Fill=�������}?�3-i��JK�3-i�`$SK�3-i0ZK`4i0ZK�u<i�0ZK�u<i�0ZK@�Di`$SK�Ki�JK�Kiѐ�}?�Ki�q=
ף��@�KiP�n?@�DiP�n?�u<i�P�n?�u<i�P�n?`4iq=
ף��@�3-i��}?�3-i$
��Left=�$	��Top=�$��199.975��$��1.998��$��	20,40,0,0q����� LinearGradientBrush_1�� �� 
StartPoint$ -1.22086e-006,0.499939&� �� EndPoint$ 0.999997,0.499939&�.��� GradientStop_1�� � Color$ 	#FFC4CFBE�� � Offset$ 0Q�� GradientStop_2��$ 	#00C4CFBE��$ 1Q��� TextBlock_4��$
 Left=�$��Segoe UI)�$
��Bold"�$��18(�$	��Top=�$��20,15,433,0q�$ 0.5,0.5&�   CPlayerClub_xaml_TextBlock_4#���  ���	#FFFFFFFF$��23.94��z�V� TransformGroup_3��.~��� ScaleTransform_3��$ 1Q�$ 1Q��� SkewTransform_3��$ 0Q�$ 0Q��� RotateTransform_3��$ 0Q�T� TranslateTransform_3��$	 0Q�$
 0Q��� Label_1��$
��Left=�$��20,72.59,0,0q�$	��Top=�$	��160��$
��Auto��  CPlayerClub_xaml_Label_1#���    	SegoeUI20#���  $��16(�  FormLabelForegroundColour#���  $
��Bold"��� Label_2��$
��Left=�$��20,116.71,0,0q�$	��181��  CPlayerClub_xaml_Label_2#���  #���  $��16(�#���  $	��Top=�$��31.28��$
��Bold"��� Label_3��$��Right=�$��0,116.71,190,0q�$	��Top=�$	��160��$
��Auto��  CPlayerClub_xaml_Label_3#���  #���  $��16(�#���  $
��Bold"��� -    ��Name$ 	txtAcctNo��	txtAcctNo��$
��Left=�$��190,75.248,0,0q�$	��Top=�$	��200��$��24��$�� �� �� TextWrapping$
 Wrap=�$��16(� 	 TextBoxStyle1#��� 	  
 TextBoxGradient#��� 
   TextBoxOutline#���  �� Label_4��$��0,75.248,150,0q�$	��Top=�$
��Auto��  CPlayerClub_xaml_Label_4#���  #���  $��16(�#���  $��Right=�$	��200��$
��Bold"��� -    ��Name$ lvRedeemPrizeList��lvRedeemPrizeList��$ �� IsSynchronizedWithCurrentItem$
 TrueY�  Bmc_ListViewStyleFixedLock#���  $��16(�$��Segoe UI)� " BMC_ListView_ListViewItemStyle#���  $��20,160,20,180q�$
��Auto=� �� View �� 
GridView_1��.P��� GridViewColumn_1�� % !CPlayerClub_xaml_GridViewColumn_1#���   �� Width$	 500�� �� CellTemplate# [  �� GridViewColumn_2�� % !CPlayerClub_xaml_GridViewColumn_2#���  $	 250��# [ �� GridViewColumn_3�� % !CPlayerClub_xaml_GridViewColumn_3#���  $	 250��# [ �� Label_5��$
��Left=�$��20,0,0,144.128q�$
��Auto��  CPlayerClub_xaml_Label_5#���  #���  $��16(�#���  $��Bottom=�$
��Auto��$
��Bold"��� Label_6��$
��Left=�$��	20,0,0,99q�$
��Auto��  CPlayerClub_xaml_Label_6#���  #���  $��16(�#���  $��Bottom=�$
��Auto��$
��Bold"��� -    ��Name$ lblPrizeDescription��lblPrizeDescription��$
��Left=�$��185,0,0,144.128q�$��88��$�� ��#���  $��16(�#���  $��Bottom=�$
��Auto���� -   $ lblUnitPointValue��lblUnitPointValue��$
��Left=�$��
185,0,0,99q�$��88�� & "CPlayerClub_xaml_lblUnitPointValue#���  #���  $��16(�#���  $��Bottom=�$
��Auto���� Label_7��$��425,0,270,133q�  CPlayerClub_xaml_Label_7#���  #���  $��16(�#���  $��Bottom=�$��37��$
��Bold"��� Label_8��$��
0,0,245,99q�  CPlayerClub_xaml_Label_8#���  #���  $��16(�#���  $��Bottom=�$��31.28��$��Right=�$	��199��$
��Bold"��� -   $ lblCashValue��lblCashValue��$��
0,0,140,99q�#���  $��16(�#���  $��Bottom=�$
��Auto��$��Right=�$	��100���� -   $ txtPrizeQty��txtPrizeQty��$��0,0,130,144.128q�   CPlayerClub_xaml_txtPrizeQty#���  $
 Wrap=�$��Right=�$	��120��$��Bottom=�$��24��#��� 	 #��� 
 #���   �� TextAlignment$
 Left=�$��16(��� -	   Button_1��$��0,0,0,15q�  
BMC_Button#���  $��Bottom=�$
��Auto��  CPlayerClub_xaml_Button_1#���  $��Center=��� -
   $��GetAccDetails��GetAccDetails��$��400,60,0,15q�#���  $	��Top=�$
��Auto�� " CPlayerClub_xaml_GetAccDetails#���  $
��Left=��� -   $ lblPlayerName��lblPlayerName��$
��Left=�$��145,116.71,0,0q�$	��160��#���  $��16(�#���  $	��Top=�$
��Auto���� -   $ lblClubState��lblClubState��$��0,116.71,95,0q�$	��Top=�$
��Auto��#���  $��16(�#���  $��Right=�$	��160���� -   $ lblPointsBal��lblPointsBal��$��0,75.248,5,0q�$	��Top=�$
��Auto��#���  $��16(�#���  $��Right=�$	��160���� -    ��Name$ btn_Up��
btn_Up��$��Right=�$��0,0,89.75,140q�$��Bottom=�$��30��$��30��  CPlayerClub_xaml_btn_Up#���    
SpinButton#���  �� -   $ btn_Down��btn_Down��$��Right=�$��0,0,43.75,140q�#���  $��Bottom=�$��30��$��30��  CPlayerClub_xaml_btn_Down#���  $ 0.5,0.5&�z�V� TransformGroup_4��.~��� ScaleTransform_4��$ 1Q�$ 1Q��� SkewTransform_4��$ 0Q�$ 0Q��� RotateTransform_4��$	 180Q�T� TranslateTransform_4��$	 0Q�$
 0Q�