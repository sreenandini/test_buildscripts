   M S B A M L   `   `   `  ����   BMC.Presentation.POS/    )BMC.Presentation.POS.PlayerCardValidation   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     �Name$  Window��
Window�� � Title$ MultiConnect��$	��600��$	��430�� � AllowsTransparency .  � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=���Z� .���� -    ��Name$
 Root��Root��   LoginBGBrush#���   $
��Auto��z�V� TransformGroup_1��.~��� ScaleTransform_1�� �� ScaleX$ 1Q� �� ScaleY$ 1Q��� SkewTransform_1�� �� AngleX$ 0Q�	 �� AngleY$	 0Q��� RotateTransform_1��
 �� Angle$
 0Q�T� TranslateTransform_1�� T� X$ 0Q� T� Y$ 0Q�.+�� -    �Name$ grdPlayerCard��grdPlayerCard��$
��Auto��$
��Auto�� �� Left$ 60��
 �� Top$	 150��  LoginInputBG#���   � RowDefinitions �� RowDefinition_1��$
��Auto � � ColumnDefinitions �� ColumnDefinition_1��$��* ��� ColumnDefinition_2��$��216.404 �.+��� -    ��Name$ txtPlayerCardNumber��txtPlayerCardNumber��$��189,44,68,35q� �� TextAlignment$
 Left=�$��0�� �� 	MaxLength$ 10��  TextBoxGradient#���    TextBoxOutline#���    TextBoxForeground#���    TextBoxStyle1#���  $��Segoe UI)�$��14(�$
��Bold"�$	 575��$	 317��$��1��$��2��$��2���� -   $��lblPlayerCardNumber��$	 255��$	 321��$��Segoe UI)�$��14(�$
��Bold"�
���   �$��29,44,98,36q�  PlayerCardNumber#���   �� TextAlignment$ Right=�$
��Wrap=�$��1��$��2���� -    ��Name$ btnValidate��btnValidate��$��61��$	��104��  PlayerCard_btn_Validate#���    ButtonStyle1#���  $��14(�$��Segoe UI)�$
��Bold"� � Cursor$
 Hand�� 	 
ListBorder#��� 	 $��3��$��Bottom=�$
��Left=�$	 180��$	 300�� �� 	IsDefault . �� -   $ btnClose��btnClose��$��61��$	��102�� 
 PlayerCard_btn_Close#��� 
 #���  $��14(�$��Segoe UI)�$
��Bold"�$
 Hand��#��� 	 $��4��$��Bottom=�$��Right=�$	 330��$	 300���� -   $ btnExit��btnExit��  SiteSetup_xaml_btnExit#���  $��1��  BMC_App_Exit#���   J� RenderTransformOrigin$ 0.455,0.272&�$��Right=�$��52��$��60��$	 550��$ 0��$��5���� TextBlock_1��$
 Left=�$��Segoe UI)�$
��Bold"�$��18(�$	��Top=�$
��Left=�$��	20,15,0,0q�$ 0.5,0.5&�  PlayerCard_xaml_Title#���  ���	#FFFFFFFF$��37��$ 40��$ 52��$	��363��z�V� TransformGroup_2��.~��� ScaleTransform_2��$ 1Q�$ 1Q��� SkewTransform_2��$ 0Q�$	 0Q��� RotateTransform_2��$
 0Q�T� TranslateTransform_2��$ 0Q�$ 0Q�F� 
Path_1�� �� Stretch$
 Fill=�������}?�3-i��JK�3-i�`$SK�3-i0ZK`4i0ZK�u<i�0ZK�u<i�0ZK@�Di`$SK�Ki�JK�Kiѐ�}?�Ki�q=
ף��@�KiP�n?@�DiP�n?�u<i�P�n?�u<i�P�n?`4iq=
ף��@�3-i��}?�3-i$	��200��$��4��$ 60��$	 108������ LinearGradientBrush_1�� �� 
StartPoint$ -1.22086e-006,0.499939&� �� EndPoint$ 0.999997,0.499939&�.��� GradientStop_1�� � Color$ 	#FFC4CFBE�� � Offset$ 0Q�� GradientStop_2��$ 	#00C4CFBE��$ 1Q�