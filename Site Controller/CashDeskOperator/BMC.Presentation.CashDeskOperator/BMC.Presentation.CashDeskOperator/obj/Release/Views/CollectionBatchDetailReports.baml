   M S B A M L   `   `   `  ����   BMC.Presentation.POS7    1BMC.Presentation.POS.CollectionBatchDetailReports   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  Collection Batch Reports��$	��210��$	��407�� � AllowsTransparency .  � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � Icon$% ..\Resources\BMC_Icon_16x16.png����Z� .���� -    ��Name$
 Root��Root��   MsgBoxBG_color#���   $	��383��$	��120��z�V� TransformGroup_1��.~��� ScaleTransform_1�� �� ScaleX$ 1Q� �� ScaleY$ 1Q��� SkewTransform_1�� �� AngleX$ 0Q�	 �� AngleY$	 0Q��� RotateTransform_1��
 �� Angle$
 0Q�T� TranslateTransform_1�� T� X$ 0Q� T� Y$ 0Q�.+�� -    �Name$ 	pnlHeader��	pnlHeader��$	��Top=�$��52�� �� Left$	 -23��
 �� Top$	 -45��$��6,0,0,0q�$	��418��.+�F� 
Path_1�� �� Stretch$
 Fill=�����p �B\�����@Q@�-\�����@S�%J-\�����@�!s-����Y�@�!s-\�����@Q�!s-�z�G#�@S�!s-�����&�@Xa�-	��%�@�-�z�G)�@Qf���z�G)�@S���
h"l�&�@t�����h�(�@���:��H#�@Q�-�\�����@S�-�����Y�@�)\�����@ �B\�����@  MsgBoxTitleColor#���  $ 1��$	 -29��$
��17,3q��� -    ��Name$ tbHeader��tbHeader��$��Collection Batch Reports��$��21,18,204,-35q�$��NoWrap=�
�������$��Segoe UI)�$��16(�$y�Visible=��� -    ��Name$ 
cboReports��
cboReports��$��15(�$	��360��  BMC_ComboBoxStyle#���  $ �� IsSynchronizedWithCurrentItem$
 TrueY�$��Segoe UI)� 	 Table#���  $��	-40,0,0,0q�$��40��$ 51��$ 13�� �� DisplayMemberPath$ Description�� �� SelectedValuePath$ ID���� -    ��Name$ btnGenerate��btnGenerate��$��40��$	��162�� $  ColBatchReports_xaml_btnGenerate#���    ButtonStyle1#���  $��14(�$��Segoe UI)�$
��Bold"� � Cursor$
 Hand��  
ListBorder#���  $��Bottom=�$
��Left=�$ 12��$ 61���� -   $ btnClose��btnClose��#���  $	 204��$ 61�� ! ColBatchReports_xaml_btnClose#���  $
 Hand��$��Segoe UI)�$��14(�$
��Bold"�$��40��#���  $	��170��