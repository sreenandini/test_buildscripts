   M S B A M L   `   `   `  ����   BMC.Presentation.POS1    +BMC.Presentation.CPositionDetailsPlayerClub   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     >�Name$  UserControl��UserControl��$	��720��$	��485�� J� RenderTransformOrigin$ 0.5,0.5&� � 	Resources    DateTemplate �� TextAlignment  EventTemplate  DescriptionTemplate��%�  &	        &	 �     &	 �    �� DataTemplate_1��.R�� 
Grid_1��$��0��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��0.5,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_1��$
 Left=�$��0,0,0,0q����� *	EventDate+�� DataTemplate_2��.R�� 
Grid_2��$��0��.+��� Border_2��
���   �$��-7,-2,-5.5,-2q�$��0.5,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_2��$
 Left=�$��0,0,0,0q����� *
Event_Type+�� DataTemplate_3��.R�� 
Grid_3��$��0��.+��� Border_3��
���   �$��-7,-2,-5.5,-2q�$��0.5,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_3��$
 Left=�$��0,0,0,0q����� *	Details+.��� -    �Name$ 
LayoutRoot��
LayoutRoot��$ 0.5,0.5&�$	��644����Z� z�V� TransformGroup_1��.~��� ScaleTransform_1�� �� ScaleX$ 1Q� �� ScaleY$ 1Q��� SkewTransform_1�� �� AngleX$ 0Q� �� AngleY$ 0Q��� RotateTransform_1��	 �� Angle$	 0Q�T� TranslateTransform_1��
 T� X$
 0Q� T� Y$ 0Q�.+��� Border_4��$��Stretch=�$��Stretch=�$
��Auto��$
��Auto�� �� CornerRadius$ 5,5,5,5�����	#19CDDD98� -   $��grid1��	grid1��$��25,12,35,134q�.+��� TextBlock_4��$
 Left=�$��Segoe UI)�$
��Bold"�$��18(�$	��Top=�$��25,15,205,0q�$ 0.5,0.5&� / +CPositionDetailsPlayerClub_xaml_TextBlock_4#���  ���	#FFFFFFFF$��23.94��z�V� TransformGroup_2��.~��� ScaleTransform_2��$ 1Q�$ 1Q��� SkewTransform_2��$ 0Q�$ 0Q��� RotateTransform_2��$	 0Q�T� TranslateTransform_2��$
 0Q�$ 0Q���  �� Orientation$ 
Horizontal=�$��0,50,10,392q�.+�	� -    	�Name$ chkPlayerClub��chkPlayerClub�� 1 -CPositionDetailsPlayerClub_xaml_chkPlayerClub#���   " bmc_General_RADIO_Style_Button#���  $��Segoe UI)�$��14(�$
��Bold"�$
��Auto��$
��Auto�� d� 	IsChecked$ FalseY�$	��Top=� 	� 	GroupName$ FullCountView���� $��10��	� -   $ chkPlayerClubBonus��chkPlayerClubBonus�� 6 2CPositionDetailsPlayerClub_xaml_chkPlayerClubBonus#���  #���  $��Segoe UI)�$��14(�$
��Bold"�$
��Auto��$
��Auto��$ FalseY�$	��Top=�$ FullCountView���� $��10���� -   $��spPlayerClub��
PlayerClub��$��	0,111,0,0q�.+�� -   $��dgPlayerClub��$��0,0,0,0q� � RowDefinitions �� $��45 ��� $��45 ��� $��45 ��� $��45 ��� $��45 ��� $��45 ��� $��45 ��� $��45 � � ColumnDefinitions �� $	��200 ��� $��* �.+��� -   $��tblVIP��
tblVIP��$��0��$��0��$��0,0,0,0q�$��NoWrap=�$��40��$	��192�� ' #CPositionDetailsPlayerClub_xaml_VIP#���  
�������$��Segoe UI)�$��16(�$y�Visible=�$��Center=�$
��Left=��� -	   $��tblPlayerName��tblPlayerName��$��0��$��1��$��0,0,0,0q�$��NoWrap=�$��40��$	��192�� * &CPositionDetailsPlayerClub_xaml_tbName#���  
�������$��Segoe UI)�$��16(�$y�Visible=�$��Center=�$
��Left=��� -
   $��	tblAcctNo��	tblAcctNo��$��0��$��2��$��0,0,0,0q�$��NoWrap=�$��40��$	��192�� -	 )CPositionDetailsPlayerClub_xaml_tblAcctNo#��� 	 
�������$��Segoe UI)�$��16(�$y�Visible=�$��Center=�$
��Left=��� -   $��tblTimeinplay��tblTimeinplay��$��0��$��3��$��0,0,0,0q�$��NoWrap=�$��40��$	��192�� 1
 -CPositionDetailsPlayerClub_xaml_tblTimeinplay#��� 
 
�������$��Segoe UI)�$��16(�$y�Visible=�$��Center=�$
��Left=��� -   $��tblClub��tblClub��$��0��$��4��$��0,0,0,0q�$��NoWrap=�$��40��$	��192�� + 'CPositionDetailsPlayerClub_xaml_tblClub#���  
�������$��Segoe UI)�$��16(�$y�Visible=�$��Center=�$
��Left=��� -   $��tblClubStatus��tblClubStatus��$��0��$��5��$��0,0,0,0q�$��NoWrap=�$��40��$	��192�� 1 -CPositionDetailsPlayerClub_xaml_tblClubStatus#���  
�������$��Segoe UI)�$��16(�$y�Visible=�$��Center=�$
��Left=��� -   $��tblBday��tblBday��$��0��$��6��$��0,0,0,0q�$��NoWrap=�$��40��$	��192�� + 'CPositionDetailsPlayerClub_xaml_tblBday#���  
�������$��Segoe UI)�$��16(�$y�Visible=�$��Center=�$
��Left=��� -   $��tblHostCode��tblHostCode��$��0��$��7��$��0,0,0,0q�$��NoWrap=�$��40��$	��192�� / +CPositionDetailsPlayerClub_xaml_tblHostCode#���  
�������$��Segoe UI)�$��16(�$y�Visible=�$��Center=�$
��Left=��� -   $��txtVIP��
txtVIP��$��1��$��0��$��	10,0,10,0q� �� TextWrapping$
 Wrap=�$��37��  TextBoxGradient#���    TextBoxOutline#���    TextBoxStyle1#���  $��Center=� �� 	MaxLength$ 30��$��16(�$��Normal"� �� TextAlignment$
 Left=�$��Tahoma)� �� 
IsReadOnly . �� -   $��txtPlayerName��txtPlayerName��$��1��$��1��$��	10,0,10,0q�$
 Wrap=�$��37��#���  #���  #���  $��Center=�$ 30��$��16(�$��Normal"�$
 Left=�$��Tahoma)� . �� -   $��txtAccountNumber��txtAccountNumber��$��1��$��2��$��	10,0,10,0q�$
 Wrap=�$��37��#���  #���  #���  $��Center=�$ 30��$��16(�$��Normal"�$
 Left=�$��Tahoma)� . �� -   $��txtTimeInPlay��txtTimeInPlay��$��1��$��3��$��	10,0,10,0q�$
 Wrap=�$��37��#���  #���  #���  $��Center=�$ 30��$��16(�$��Normal"�$
 Left=�$��Tahoma)� . �� -   $��txtClubState��txtClubState��$��1��$��4��$��	10,0,10,0q�$
 Wrap=�$��37��#���  #���  #���  $��Center=�$ 30��$��16(�$��Normal"�$
 Left=�$��Tahoma)� . �� -   $��txtClubStatus��txtClubStatus��$��1��$��5��$��	10,0,10,0q�$
 Wrap=�$��37��#���  #���  #���  $��Center=�$ 30��$��16(�$��Normal"�$
 Left=�$��Tahoma)� . �� -   $��txtBirthday��txtBirthday��$��1��$��6��$��	10,0,10,0q�$
 Wrap=�$��37��#���  #���  #���  $��Center=�$ 30��$��16(�$��Normal"�$
 Left=�$��Tahoma)� . �� -   $��txtHostName��txtHostName��$��1��$��7��$��	10,0,10,0q�$
 Wrap=�$��37��#���  #���  #���  $��Center=�$ 30��$��16(�$��Normal"�$
 Left=�$��Tahoma)� . �� -   $��spPlayerClubBonus��PlayerClubBonus��$��	0,111,0,0q�$y�	Collapsed=�.+�� -   $��dgPlayerClubBonus��$��0,0,0,0q� �� $��50 ��� $��50 ��� $��50 ��� $��50 ��� $��50 ��� $��50 � �� $	��200 ��� $��* �.+��� -   $��tblBonusPoints��tblBonusPoints��$��0��$��0��$��0,10,0,0q�$��NoWrap=�$��40��$	��192�� 2 .CPositionDetailsPlayerClub_xaml_tblBonusPoints#���  
�������$��Segoe UI)�$��16(�$y�Visible=�$��Center=�$
��Left=��� -   $��tblBonusMultiPlayer��tblBonusMultiPlayer��$��0��$��1��$��0,10,0,0q�$��NoWrap=�$��40��$	��190�� 7 3CPositionDetailsPlayerClub_xaml_tblBonusMultiPlayer#���  
�������$��Segoe UI)�$��16(�$y�Visible=�$��Center=�$
��Left=��� -   $��tblBonusEffectDate��tblBonusEffectDate��$��0��$��2��$��0,10,0,0q�$��NoWrap=�$��40��$	��192�� 6 2CPositionDetailsPlayerClub_xaml_tblBonusEffectDate#���  
�������$��Segoe UI)�$��16(�$y�Visible=�$��Center=�$
��Left=��� -   $��tblBonusEffectTime��tblBonusEffectTime��$��0��$��3��$��0,10,0,0q�$��NoWrap=�$��40��$	��192�� 6 2CPositionDetailsPlayerClub_xaml_tblBonusEffectTime#���  
�������$��Segoe UI)�$��16(�$y�Visible=�$��Center=�$
��Left=��� -   $��tblBonusExpireTime��tblBonusExpireTime��$��0��$��4��$��0,10,0,0q�$��NoWrap=�$��40��$	��192�� 6 2CPositionDetailsPlayerClub_xaml_tblBonusExpireTime#���  
�������$��Segoe UI)�$��16(�$y�Visible=�$��Center=�$
��Left=��� -   $��txtBonusPoints��txtBonusPoints��$��1��$��0��$��	10,5,10,5q�$
 Wrap=�$��40��#���  #���  #���  $	��Top=�$ 30��$��16(�$��Normal"�$
 Left=�$��Tahoma)� . �� -    $��txtBonusMultiPlayer��txtBonusMultiPlayer��$��1��$��1��$��	10,5,10,5q�$
 Wrap=�$��40��#���  #���  #���  $	��Top=�$ 30��$��16(�$��Normal"�$
 Left=�$��Tahoma)� . �� -!   $��txtBonusEffectDate��txtBonusEffectDate��$��1��$��2��$��	10,5,10,5q�$
 Wrap=�$��40��#���  #���  #���  $	��Top=�$ 30��$��16(�$��Normal"�$
 Left=�$��Tahoma)� . �� -"   $��txtBonusEffectTime��txtBonusEffectTime��$��1��$��3��$��	10,5,10,5q�$
 Wrap=�$��40��#���  #���  #���  $	��Top=�$ 30��$��16(�$��Normal"�$
 Left=�$��Tahoma)� . �� -#   $��txtBonusExpireTime��txtBonusExpireTime��$��1��$��4��$��	10,5,10,5q�$
 Wrap=�$��40��#���  #���  #���  $	��Top=�$ 30��$��16(�$��Normal"�$
 Left=�$��Tahoma)� . �� -$   $��	lblStatus��	lblStatus��$��2��$��5��$��40��$
��Auto��$��0,10,0,0q�
�������$��Segoe UI)�$��16(�$y�Visible=�$��Center=�$��Center=��� -%   $��	PlayerIco��	PlayerIco��$
��None=�$��0,8,15,0q�$��48��$	��Top=�$��Right=�$��75��F� 
Path_1�� �� Stretch$
 Fill=�������}?�3-i��JK�3-i�`$SK�3-i0ZK`4i0ZK�u<i�0ZK�u<i�0ZK@�Di`$SK�Ki�JK�Kiѐ�}?�Ki�q=
ף��@�KiP�n?@�DiP�n?�u<i�P�n?�u<i�P�n?`4iq=
ף��@�3-i��}?�3-i$
��Left=�$	��Top=�$��199.975��$��1.998��$��	20,-4,0,0q����� LinearGradientBrush_1�� �� 
StartPoint$ -1.22086e-006,0.499939&� �� EndPoint$ 0.999997,0.499939&�.��� GradientStop_1�� � Color$ 	#FFC4CFBE�� � Offset$ 0Q�� GradientStop_2��$ 	#00C4CFBE��$ 1Q�