   M S B A M L   `   `   `  ����   BMC.Presentation.POS-    'BMC.Presentation.CPositionCurrentStatus   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     >�Name$  UserControl��UserControl��$	��870��$	��660�� J� RenderTransformOrigin$ 0.5,0.5&� � 	Resources  !  BMC_Enterprise_StatusTemplate ��Name �� Triggers �� Value �� Binding �� Path �� 
TargetName	 �� Property
 �� Value  PosTemplate ��Name �� TextAlignment  VLTAAMSTemplate  VLTVerificationTemplate " BAD_AAMS_EnableDisableTemplate 5 1PosCurrentStatus_xaml_LabelBAD_AAMS_EnableDisable ! GameInstallAAMSStatusTemplate   GameEnableAAMSStatusTemplate  GameVerificationTemplate 	 GameNameTemplate��%�  &	        &	 H    &	 *    &	 d    &	 �    &	 �
    &	 ?    &	 �    &		 �    �� DataTemplate_10��.R�� Grid_10��$��10��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $ Image1��
Image1��$��16��$��16��$y�	Collapsed=�$��..\Images\Success.png���� $ Image2��
Image2��$��16��$��16��$y�	Collapsed=�$��..\Images\Failure.png�� �� DataTrigger_1��$ 1�� �� $ BMC_Enterprise_Status�.a��� Setter_1��$ Image1��	 � y�$
 Visible=��� DataTrigger_2��$ 0�� �� $ BMC_Enterprise_Status�.a��� Setter_2��$ Image2��	 � y�$
 Visible=��� DataTemplate_2��.R�� 
Grid_2��$��5��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $ Bar_Pos_Name��Bar_Pos_Name��$ Right=����� *Bar_Pos_Name+�� DataTemplate_3��.R�� 
Grid_3��$��10��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $ Image1��
Image1��$��16��$��16��$y�	Collapsed=�$��..\Images\Success.png���� $ Image2��
Image2��$��16��$��16��$y�	Collapsed=�$��..\Images\Failure.png�� �� DataTrigger_1��$ 1�� �� $ BAD_AAMS_Status�.a��� Setter_1��$ Image1��	 � y�$
 Visible=��� DataTrigger_2��$ 0�� �� $ BAD_AAMS_Status�.a��� Setter_2��$ Image2��	 � y�$
 Visible=��� DataTemplate_4��.R�� 
Grid_4��$��10��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $ Image1��
Image1��$��16��$��16��$y�	Collapsed=�$��..\Images\Success.png���� $ Image2��
Image2��$��16��$��16��$y�	Collapsed=�$��..\Images\Failure.png�� �� DataTrigger_1��$ 1�� �� $ BAD_Verification_Status�.a��� Setter_1��$ Image1��	 � y�$
 Visible=��� DataTrigger_2��$ 0�� �� $ BAD_Verification_Status�.a��� Setter_2��$ Image2��	 � y�$
 Visible=��� DataTemplate_8��.R�� 
Grid_8��$��10��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $ Image1��
Image1��$��16��$��16��$y�	Collapsed=�$��..\Images\Success.png���� $ Image2��
Image2��$��16��$��16��$y�	Collapsed=�$��..\Images\Failure.png���� $! LabelBAD_AAMS_EnableDisable��LabelBAD_AAMS_EnableDisable��$y�	Collapsed=�$ Center=�#���   �� DataTrigger_1��$ 1�� �� $ BAD_AAMS_EnableDisable�.a��� Setter_1��$ Image1��	 � y�$
 Visible=��� DataTrigger_2��$ 2�� �� $ BAD_AAMS_EnableDisable�.a��� Setter_2��$ Image2��	 � y�$
 Visible=��� DataTrigger_3��$ 0�� �� $ BAD_AAMS_EnableDisable�.a��� Setter_3��$! LabelBAD_AAMS_EnableDisable��	 � y�$
 Visible=��� DataTemplate_5��.R�� 
Grid_5��$��10��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $ Image1��
Image1��$��16��$��16��$y�	Collapsed=�$��..\Images\Success.png���� $ Image2��
Image2��$��16��$��16��$y�	Collapsed=�$��..\Images\Failure.png�� �� DataTrigger_1��$ 1�� �� $ Game_Install_AAMS_Status�.a��� Setter_1��$ Image1��	 � y�$
 Visible=��� DataTrigger_2��$ 0�� �� $ Game_Install_AAMS_Status�.a��� Setter_2��$ Image2��	 � y�$
 Visible=��� DataTemplate_9��.R�� 
Grid_5��$��10��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $ Image1��
Image1��$��16��$��16��$y�	Collapsed=�$��..\Images\Success.png���� $ Image2��
Image2��$��16��$��16��$y�	Collapsed=�$��..\Images\Failure.png�� �� DataTrigger_1��$ 1�� �� $ Game_Enable_AAMS_Status�.a��� Setter_1��$ Image1��	 � y�$
 Visible=��� DataTrigger_2��$ 0�� �� $ Game_Enable_AAMS_Status�.a��� Setter_2��$ Image2��	 � y�$
 Visible=��� DataTemplate_6��.R�� 
Grid_6��$��10��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $ Image1��
Image1��$��16��$��16��$y�	Collapsed=�$��..\Images\Success.png���� $ Image2��
Image2��$��16��$��16��$y�	Collapsed=�$��..\Images\Failure.png�� �� DataTrigger_1��$ 1�� �� $ Game_Verification�.a��� Setter_1��$ Image1��	 � y�$
 Visible=��� DataTrigger_2��$ 0�� �� $ Game_Verification�.a��� Setter_2��$ Image2��	 � y�$
 Visible=��� DataTemplate_7��.R�� 
Grid_7��$��10��.+��� Border_1��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� $ Machine_Name��Machine_Name������ *Machine_Name+.��� .+��� -    ��Name$ lstPosCurrent��lstPosCurrent��$��Visible=�$��13,60,20,86q�$ �� IsSynchronizedWithCurrentItem$
 TrueY� 
 Bmc_ListViewStyleFixedLock#��� 
 $��13(�$
��Auto��$��Stretch=�$��Segoe UI)� " BMC_ListView_ListViewItemStyle#���   �� SelectionMode$ Single=�����  �� Mode$ Default=� �� View �� 
GridView_1��.P��� GridViewColumn_1�� $  PosCurrentStatus_xaml_ghPosition#���   �� Width$ 80�� �� CellTemplate# [ �� GridViewColumn_2��   PosCurrentStatus_xaml_ghGame#���  $	 180��# [	 �� GridViewColumn_3�� + 'PosCurrentStatus_xaml_ghVLTVerification#���  $	 120��# [ �� GridViewColumn_4�� ) %PosCurrentStatus_xaml_ghVLTAAMSStatus#���  $	 120��# [ �� GridViewColumn_5�� , (PosCurrentStatus_xaml_ghGameVerification#���  $	 125��# [ �� GridViewColumn_6�� 1 -PosCurrentStatus_xaml_ghGameInstallAAMSStatus#���  $	 170��# [ �� GridViewColumn_8�� 0 ,PosCurrentStatus_xaml_ghGameEnableAAMSStatus#���  $	 180��# [ �� GridViewColumn_7�� . *PosCurrentStatus_xaml_ghAAMS_EnableDisable#���  $	 170��# [ �� GridViewColumn_8�� 1 -PosCurrentStatus_xaml_ghBMC_Enterprise_Status#���  $	 170��# [  �� TextBlock_1��$
 Left=�$��Segoe UI)�$
��Bold"�$��18(�$	��Top=�$
��Left=�$��	20,15,0,0q�$ 0.5,0.5&� # PosCurrentStatus_xaml_txtHeader#���  ���	#FFFFFFFFF� 
Path_2�� �� Stretch$
 Fill=�������}?�3-i��JK�3-i�`$SK�3-i0ZK`4i0ZK�u<i�0ZK�u<i�0ZK@�Di`$SK�Ki�JK�Kiѐ�}?�Ki�q=
ף��@�KiP�n?@�DiP�n?�u<i�P�n?�u<i�P�n?`4iq=
ף��@�3-i��}?�3-i$
��Left=�$	��Top=�$��199.975��$��1.998��$��	20,40,0,0q����� LinearGradientBrush_1�� �� 
StartPoint$ -1.22086e-006,0.499939&� �� EndPoint$ 0.999997,0.499939&�.��� GradientStop_1�� � Color$ 	#FFC4CFBE�� � Offset$ 0Q�� GradientStop_2��$ 	#00C4CFBE��$ 1Q�	� -    	�Name$ chkVLTAAMSVerification��chkVLTVerification��  Chkbox_Radio_Style#���  $
��Auto��$
��Auto��
����ح� , (PosCurrentStatus_xaml_chkVLTVerification#���  {�. $y�Visible=�$��	15,0,0,35q�$
��Left=�$��Bottom=�	� -   $ chkVLTAAMSStatus��
chkVLTAAMS��#���  $
��Auto��$
��Auto��
����ح� * &PosCurrentStatus_xaml_chkVLTAAMSStatus#���  {�. $y�Visible=�$��
245,0,0,35q�$
��Left=�$��Bottom=�	� -   $ chkGameVerification��chkGameVerification��#���  $
��Auto��$
��Auto��
����ح� - )PosCurrentStatus_xaml_chkGameVerification#���  {�. $y�Visible=�$��
440,0,0,35q�$
��Left=�$��Bottom=�	� -   $ chkGameInstallAAMS��chkGameInstallAAMS��#���  $
��Auto��$
��Auto��
����ح�#���  {�. $y�Visible=�$��
660,0,0,35q�$
��Left=�$��Bottom=�	� -   $ chkGameEnableAAMS��chkGameEnableAAMS��#���  $
��Auto��$
��Auto��
����ح�#���  {�. $y�Visible=�$��15,0,0,5q�$
��Left=�$��Bottom=�	� -   $ chkAAMSEnableDiable��chkAAMSEnableDisable��#���  $
��Auto��$
��Auto��
����ح�#���  {�. $y�Visible=�$��	245,0,0,5q�$
��Left=�$��Bottom=�	� -	   $ chkBMCEnterpriseStatus��chkBMCEnterpriseStatus��#���  $
��Auto��$
��Auto��
����ح�#���  {�. $y�Visible=�$��	440,0,0,5q�$
��Left=�$��Bottom=�	� -
   $ chkAll��
chkAll��#���  $
��Auto��$
��Auto��
����ح� ) %PosCurrentStatus_xaml_chkAllPositions#���  {�. $y�Visible=�$��	660,0,0,5q�$
��Left=�$��Bottom=� d� 	IsChecked$
 TrueY�