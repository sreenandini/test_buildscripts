   M S B A M L   `   `   `  ����   BMC.Presentation.POS'    !BMC.Presentation.MachineEnrolment   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     �Name$  Window��
Window�� � Title$ MachineEnrolment��$��665.75��$	��634�� � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � ShowInTaskbar .  ��Z� .��� -    �Name$ 
LayoutRoot��
LayoutRoot��$	��642�� � ColumnDefinitions �� ColumnDefinition_1��$
��0.3* ��� ColumnDefinition_2��$
��0.7* � � RowDefinitions �� RowDefinition_1��$��51.333 ��� RowDefinition_2��$	��600 �.+��� Border_1��$��0,0,0,17.333q�$��2��$��2��   PositionDetails_BG#���   ���	#FF000000$��1,1,1,1q�	 �� CornerRadius$	 10,10,10,10���� Border_2��  TitleBorder#���  $��0,0,0,0q�$��2�����	#FF000000$��1.5,1.5,1.5,1q�$	 	10,10,5,5��$y�Visible=��� -   
 ��Name$
 	lbl_Title��	lbl_Title��$��15,8,51.097,8q�$
��Auto��$��2��$��Segoe UI)�$��22(�$
��Bold"����	#FFFFFFFF # MachineEnrolment_xaml_lbl_Title#���  $
��Wrap=��� -    ��Name$ btn_Exit��btn_Exit��$��Right=�$��0,4.75,4.347,0q�$��42.75��$�� ��$��1��  BMC_App_Exit#���  �� TextBlock_1��$��Stretch=�$��	30,23,0,0q�$	��Top=�$
��Auto��$
��Auto�� J� RenderTransformOrigin$ 0.5,0.5&�$��1��$��Segoe UI)�$��18(�$
��Bold"� % !MachineEnrolment_xaml_TextBlock_1#���  $
��Wrap=�z�V� TransformGroup_1��.~��� ScaleTransform_1�� �� ScaleX$ 1Q� �� ScaleY$ 1Q��� SkewTransform_1�� �� AngleX$ 0Q� �� AngleY$ 0Q��� RotateTransform_1�� �� Angle$ 0Q�T� TranslateTransform_1�� T� X$ 0Q� T� Y$ 0Q��� TextBlock_2��$��Stretch=�$��	30,67,0,0q�$	��Top=�$
��Auto��$
��Auto��$ 0.5,0.5&�$��Segoe UI)�$��18(�$
��Bold"� % !MachineEnrolment_xaml_TextBlock_2#���  $
��Wrap=�$��1��z�V� TransformGroup_2��.~��� ScaleTransform_2��$ 1Q�$ 1Q��� SkewTransform_2��$ 0Q�$ 0Q��� RotateTransform_2��$ 0Q�T� TranslateTransform_2��$ 0Q�$ 0Q��� -    ��Name$ txt_Position��txt_Position��$��	4,23,15,0q� ( $IssueCashabeVerifyTicketTextboxStyle#���  $	��Top=�$
��Auto��$��1��$��1��$�� �� �� TextWrapping$
 Wrap=� �� 
IsReadOnly . �� -   $ txt_MC_AssetNo��txt_MC_AssetNo��$��	4,67,15,0q�#���  $	��Top=�$
��Auto��$�� ��$
 Wrap=�$��1��$��1���� -   $ 
txt_GMU_No��
txt_GMU_No��$��
0,111,15,0q�#���  $	��Top=�$
��Auto��$�� ��$
 Wrap=�$��Right=�$��142.525��$��1��$��1���� TextBlock_3��$��Stretch=�$��
30,111,0,0q�$	��Top=�$
��Auto��$
��Auto��$ 0.5,0.5&�$��Segoe UI)�$��18(�$
��Bold"� % !MachineEnrolment_xaml_TextBlock_3#���  $
��Wrap=�$��1��z�V� TransformGroup_3��.~��� ScaleTransform_3��$ 1Q�$ 1Q��� SkewTransform_3��$ 0Q�$ 0Q��� RotateTransform_3��$ 0Q�T� TranslateTransform_3��$ 0Q�$ 0Q��� TextBlock_4��$��206,111,159,0q�$	��Top=�$
��21.6��$ 0.5,0.5&�$��Segoe UI)�$��18(�$
��Bold"� % !MachineEnrolment_xaml_TextBlock_4#���  $
��Wrap=�$��1��$��1��z�V� TransformGroup_4��.~��� ScaleTransform_4��$ 1Q�$ 1Q��� SkewTransform_4��$ 0Q�$ 0Q��� RotateTransform_4��$ 0Q�T� TranslateTransform_4��$ 0Q�$ 0Q�� 
Grid_1��$��15,176.1,15,95.5q�$��2��$��1����Z�  �� ColumnDefinition_3��$��0.295* ��� ColumnDefinition_4��$��0.705* �.+��� Border_3��$��Stretch=�$	��Top=�$
��Auto��$��159.9��$	 10,10,10,10��$��1,1,1,1q����	#FF000000 	 InnerBG_UserControl#��� 	 $��2���� TextBlock_5��$	��Top=�$
��Auto��$ 0.5,0.5&�$��Segoe UI)�$��18(�$
��Bold"� %
 !MachineEnrolment_xaml_TextBlock_5#��� 
 $
��Wrap=�$��8,8,9.775,0q�z�V� TransformGroup_5��.~��� ScaleTransform_5��$ 1Q�$ 1Q��� SkewTransform_5��$ 0Q�$ 0Q��� RotateTransform_5��$ 0Q�T� TranslateTransform_5��$ 0Q�$ 0Q��� -   $ txt_Manufacturer��txt_Manufacturer��#���  $
��Auto��$�� ��$
 Wrap=�$��Stretch=�$��8,117.8,8,0q�$	��Top=�$
��Auto��$ 0.5,0.5&�$��1��{�. |�.  J� IsHitTestVisible . ��.   . .��z�V� TransformGroup_6��.~��� ScaleTransform_6��$ 1Q�$ 1Q��� SkewTransform_6��$ 0Q�$ 0Q��� RotateTransform_6��$ 0Q�T� TranslateTransform_6��$ 0Q�$ 0Q�.���� -	   $ txt_Mc_Type��txt_Mc_Type��#���  $�� ��$
 Wrap=�$��Stretch=�$��
8,81.2,8,0q�$	��Top=�$
��Auto��$
��Auto��$ 0.5,0.5&�$��1��{�. |�.  . ��.   . .��z�V� TransformGroup_7��.~��� ScaleTransform_7��$ 1Q�$ 1Q��� SkewTransform_7��$ 0Q�$ 0Q��� RotateTransform_7��$ 0Q�T� TranslateTransform_7��$ 0Q�$ 0Q�.���� -
   $ txt_Mc_Alt_Serial_No��txt_Mc_Alt_Serial_No��#���  $
��Auto��$�� ��$
 Wrap=�$��Stretch=�$��
8,44.6,8,0q�$	��Top=�$
��Auto��$ 0.5,0.5&�$��1��{�. |�.  . ��.   . .��z�V� TransformGroup_8��.~��� ScaleTransform_8��$ 1Q�$ 1Q��� SkewTransform_8��$ 0Q�$ 0Q��� RotateTransform_8��$ 0Q�T� TranslateTransform_8��$ 0Q�$ 0Q�.���� -   $ txt_Mc_Serial_No��txt_Mc_Serial_No��#���  $
��Auto��$�� ��$
 Wrap=�$��8,8,8,0q�$	��Top=�$ 0.5,0.5&�$��1��{�. |�.  . ��.   . .��z�V� TransformGroup_9��.~��� ScaleTransform_9��$ 1Q�$ 1Q��� SkewTransform_9��$ 0Q�$ 0Q��� RotateTransform_9��$ 0Q�T� TranslateTransform_9��$ 0Q�$ 0Q�.���� TextBlock_6��$	��Top=�$
��43.2��$ 0.5,0.5&�$��Segoe UI)�$��18(�$
��Bold"� % !MachineEnrolment_xaml_TextBlock_6#���  $
��Wrap=�$��8,47,0,0q�$��2��$
��Left=�$	��175��z�V� TransformGroup_10��.~��� ScaleTransform_10��$ 1Q�$ 1Q��� SkewTransform_10��$ 0Q�$ 0Q��� RotateTransform_10��$ 0Q�T� TranslateTransform_10��$ 0Q�$ 0Q��� TextBlock_7��$	��Top=�$
��21.6��$ 0.5,0.5&�$��Segoe UI)�$��18(�$
��Bold"� % !MachineEnrolment_xaml_TextBlock_7#���  $
��Wrap=�$��8,85,9.775,0q�z�V� TransformGroup_11��.~��� ScaleTransform_11��$ 1Q�$ 1Q��� SkewTransform_11��$ 0Q�$ 0Q��� RotateTransform_11��$ 0Q�T� TranslateTransform_11��$ 0Q�$ 0Q��� TextBlock_8��$	��Top=�$
��21.6��$ 0.5,0.5&�$��Segoe UI)�$��18(�$
��Bold"� % !MachineEnrolment_xaml_TextBlock_8#���  $
��Wrap=�$��8,120,9.775,0q�z�V� TransformGroup_12��.~��� ScaleTransform_12��$ 1Q�$ 1Q��� SkewTransform_12��$ 0Q�$ 0Q��� RotateTransform_12��$ 0Q�T� TranslateTransform_12��$ 0Q�$ 0Q��� Border_4��$��Stretch=�$
��Auto��$	 10,10,10,10��$��1,1,1,1q����	#FF000000#��� 	 $��
0,0,0,15.6q�$��Bottom=�$��128.8��$��2���� TextBlock_9��$
��Auto��$ 0.5,0.5&�$��Segoe UI)�$��18(�$
��Bold"� % !MachineEnrolment_xaml_TextBlock_9#���  $
��Wrap=�$��8,0,9.775,112.46q�$��Bottom=�z�V� TransformGroup_13��.~��� ScaleTransform_13��$ 1Q�$ 1Q��� SkewTransform_13��$ 0Q�$ 0Q��� RotateTransform_13��$ 0Q�T� TranslateTransform_13��$ 0Q�$ 0Q��� -   $ txt_Game��txt_Game��#���  $�� ��$
 Wrap=�$��Stretch=�$��
8,0,8,30.6q�$
��Auto��$
��Auto��$ 0.5,0.5&�$��Bottom=�$��1��{�. |�.  . ��.   . .��z�V� TransformGroup_14��.~��� ScaleTransform_14��$ 1Q�$ 1Q��� SkewTransform_14��$ 0Q�$ 0Q��� RotateTransform_14��$ 0Q�T� TranslateTransform_14��$ 0Q�$ 0Q�.���� -   $ txt_GameCat��txt_GameCat��#���  $
��Auto��$�� ��$
 Wrap=�$��Stretch=�$��
8,0,8,67.2q�$
��Auto��$ 0.5,0.5&�$��Bottom=�$��1��{�. |�.  . ��.   . .��z�V� TransformGroup_15��.~��� ScaleTransform_15��$ 1Q�$ 1Q��� SkewTransform_15��$ 0Q�$ 0Q��� RotateTransform_15��$ 0Q�T� TranslateTransform_15��$ 0Q�$ 0Q�.���� -   $ txt_GameCode��txt_GameCode��#���  $
��Auto��$�� ��$
 Wrap=�$��8,0,8,103.8q�$ 0.5,0.5&�$��Bottom=�$��1��{�. |�.  . ��.   . .��z�V� TransformGroup_16��.~��� ScaleTransform_16��$ 1Q�$ 1Q��� SkewTransform_16��$ 0Q�$ 0Q��� RotateTransform_16��$ 0Q�T� TranslateTransform_16��$ 0Q�$ 0Q�.���� TextBlock_10��$
��43.2��$ 0.5,0.5&�$��Segoe UI)�$��18(�$
��Bold"� & "MachineEnrolment_xaml_TextBlock_10#���  $
��Wrap=�$��8,0,0,50q�$��Bottom=�$��2��$
��Left=�$	��175��z�V� TransformGroup_17��.~��� ScaleTransform_17��$ 1Q�$ 1Q��� SkewTransform_17��$ 0Q�$ 0Q��� RotateTransform_17��$ 0Q�T� TranslateTransform_17��$ 0Q�$ 0Q��� TextBlock_11��$
��Auto��$ 0.5,0.5&�$��Segoe UI)�$��18(�$
��Bold"� & "MachineEnrolment_xaml_TextBlock_11#���  $
��Wrap=�$��8,0,9.775,39.26q�$��Bottom=�z�V� TransformGroup_18��.~��� ScaleTransform_18��$ 1Q�$ 1Q��� SkewTransform_18��$ 0Q�$ 0Q��� RotateTransform_18��$ 0Q�T� TranslateTransform_18��$ 0Q�$ 0Q��� -    ��Name$ panel_Enrol_Button��panel_Enrol_Button��$��26.4,0,229.625,30q�$��Bottom=�$
��Auto��$
��Auto��$��1��$��1�� �� Orientation$ 
Horizontal=�.+��� -   $��btnEnrol��btnEnrol��  
BMC_Button#���  $
��Auto��$
��Auto�� " MachineEnrolment_xaml_btnEnrol#���  �� -   $��	btnCancel��	btnCancel��$��10,0,0,0q�#���  $
��Auto��$
��Auto�� # MachineEnrolment_xaml_btnCancel#���  �� -   $ panel_Remove_Button��panel_Remove_Button��$��-15.85,0,161.875,30q�$��Bottom=�$
��Auto��$
��Auto��$ 
Horizontal=�$��1��$��1��.+��� -   $��	btnremove��	btnremove��#���  $
��Auto��$
��Auto�� # MachineEnrolment_xaml_btnremove#���  �� -   $��btnChangeGame��btnChangeGame��$��10,0,0,0q�#���  $
��Auto��$
��Auto�� ' #MachineEnrolment_xaml_btnChangeGame#���  $y�Hidden=��� -   $��btnRemoveCancel��btnRemoveCancel��#���  $	��100��$��55�� ) %MachineEnrolment_xaml_btnRemoveCancel#���  $��10,0,0,0q��� -    ��Name$ com_GMU_Type��com_GMU_Type��$
��Left=�$��	8,111,0,0q�$	��Top=�$��185.275��$
��32.6��$ �� IsSynchronizedWithCurrentItem$
 TrueY�$��1��$��1��  BMC_ComboBoxStyle#���  