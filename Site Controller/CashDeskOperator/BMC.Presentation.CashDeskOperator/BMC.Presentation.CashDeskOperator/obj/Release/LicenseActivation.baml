   M S B A M L   `   `   `  ����   BMC.Presentation.POS(    "BMC.Presentation.LicenseActivation   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     �Name$  Window��
Window�� � Title$ MultiConnect��$
��1024��$	��768�� � AllowsTransparency .  � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � Icon$ BMC_Icon_16x16.png����Z� .���� -    ��Name$
 Root��Root��   LoginBGBrush#���   z�V� TransformGroup_1��.~��� ScaleTransform_1�� �� ScaleX$ 1Q� �� ScaleY$ 1Q��� SkewTransform_1��	 �� AngleX$	 0Q�
 �� AngleY$
 0Q��� RotateTransform_1�� �� Angle$ 0Q�T� TranslateTransform_1�� T� X$ 0Q� T� Y$ 0Q�.+�� -    �Name$ grdLoginFields��grdLoginFields��$	��482��$	��201�� �� Left$	 296��
 �� Top$	 275��  LoginInputBG#���   � RowDefinitions �� RowDefinition_1��$��45 ��� RowDefinition_2��$��55 ��� RowDefinition_3��$��56.331 ��� RowDefinition_4��$��44.669* � � ColumnDefinitions �� ColumnDefinition_1��$��* ��� ColumnDefinition_2��$��216.404 �.+��� -    ��Name$ 
lblMessage��
lblMessage��$��6,6,6,25q�$��Segoe UI)�
���   �$��16(�$�� ��$
��Bold"�$
��Wrap=� �� TextAlignment$
 Left=�$	 255��$	 321��$��2��$��2��{�. $y�Visible=��� -    ��Name$ txtLicenseKey��txtLicenseKey��$��189,44,68,35q� �� TextAlignment$
 Left=�$��0�� �� 	MaxLength$ 15��  TextBoxGradient#���    TextBoxOutline#���    TextBoxForeground#���    TextBoxStyle1#���  $��Segoe UI)�$��14(�$
��Bold"�$	 575��$	 317��$��1��$��2��$��2���� -   $��lblKey��$	 255��$	 321��$��Segoe UI)�$��16(�$
��Bold"�
���   �$��29,44,98,36q� ! LicenseActivation_xaml_lblKey#���  $ Right=�$
��Wrap=�$��1��$��2���� -    ��Name$ btnActivate��btnActivate��$��61��$	��104�� & "LicenseActivation_xaml_btnActivate#���    ButtonStyle1#���  $��14(�$��Segoe UI)�$
��Bold"� � Cursor$
 Hand�� 	 
ListBorder#��� 	 $��3��$��Bottom=�$
��Left=�$	 398��$	 496���� -   $ btnClose��btnClose��$��61��$	��102�� #
 LicenseActivation_xaml_btnClose#��� 
 #���  $��14(�$��Segoe UI)�$
��Bold"�$
 Hand��#��� 	 $��4��$��Bottom=�$��Right=�$	 583��$	 496���� -	   $ btnExit��btnExit��  SiteSetup_xaml_btnExit#���  $��1��  BMC_App_Exit#���   J� RenderTransformOrigin$ 0.455,0.272&�$��Right=�$��52��$��60��$	 970��$ 0��$��5���� TextBlock_1��$
 Left=�$��Segoe UI)�$
��Bold"�$��18(�$	��Top=�$
��Left=�$��	20,15,0,0q�$ 0.5,0.5&� $  LicenseActivation_xaml_TextBlock#���  ���	#FFFFFFFF$��37��$ 52��$ 52��$	��363��z�V� TransformGroup_2��.~��� ScaleTransform_2��$ 1Q�$ 1Q��� SkewTransform_2��$	 0Q�$
 0Q��� RotateTransform_2��$ 0Q�T� TranslateTransform_2��$ 0Q�$ 0Q�F� 
Path_1�� �� Stretch$
 Fill=�������}?�3-i��JK�3-i�`$SK�3-i0ZK`4i0ZK�u<i�0ZK�u<i�0ZK@�Di`$SK�Ki�JK�Kiѐ�}?�Ki�q=
ף��@�KiP�n?@�DiP�n?�u<i�P�n?�u<i�P�n?`4iq=
ף��@�3-i��}?�3-i$	��298��$��4��$ 74��$	 108������ LinearGradientBrush_1�� �� 
StartPoint$ -1.22086e-006,0.499939&� �� EndPoint$ 0.999997,0.499939&�.��� GradientStop_1�� � Color$ 	#FFC4CFBE�� � Offset$ 0Q�� GradientStop_2��$ 	#00C4CFBE��$ 1Q��� StatusBar_1��$��28��$
��1024��$ 1��$	 737������ ItemsPanelTemplate_1��.R�� 
Grid_1�� �� RowDefinition_5��$��* � �� ColumnDefinition_3��$
��Auto ��� ColumnDefinition_4��$
��Auto �.@��� StatusBarItem_1��$ 2��$	 725��$��0��.���� -
   $ statusbaritemlblStatus��statusbaritemlblStatus��$��0��$��21��$	��736��
������� ) %SiteSetup_xaml_statusbaritemlblStatus#���  �� StatusBarItem_2��$ 20��$	 682��$��1��.��� -     �Name$  statusbaritempbStatus��statusbaritempbStatus��$��1��! � Value$! 10Q�$	��267��$��16��