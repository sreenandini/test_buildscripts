   M S B A M L   `   `   `  ����   BMC.Presentation.POS.    (BMC.Presentation.POS.Views.CFactoryReset   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -   UserControl_1��$	��240��$	��415����Z� .���� -     ��Name$
  Root��Root��$��0q�$	��240��$	��417��   LoginInputBG#���   .+�� -    �Name$ 	pnlHeader��	pnlHeader��$	��Top=�$��38��$��2��$	��417�� � ColumnDefinitions �� ColumnDefinition_1��$	��19* ��� ColumnDefinition_2��$��372.486* ��� -    ��Name$ lblAuthCode��lblAuthCode��$��6,50,0,0q�$��Segoe UI)�$��15(�
���   � " CFactoryReset_xaml_lblAuthCode#���  $
��Bold"�$
��Wrap=� �� TextAlignment$
 Left=�$��62��$	��Top=�$
��Left=�$	��160�� �� Left$ 6��
 �� Top$ 23���� -    ��Name$ 
txtAutCode��
txtAutCode��$��
142,50,6,0q�  TextBoxStyle1#���  $��0��$��38��$	��Top=�$ 37��$ 22��$	��220�� �� 	MaxLength$ 27��	 �� TextAlignment$
	 Left=�$��2��$��2��$��Segoe UI)�$��14(�$
��Bold"�  TextBoxForeground#���  �� -   $ 	lblStatus��	lblStatus��$��	6,130,0,0q�$y�Hidden=�$��Segoe UI)�$��15(�
���   �$��Current Progress:��$
��Bold"�$
��Wrap=�$
 Left=�$	��Top=�$
��Left=�$ 6��$ 23���� -   
 ��Name$
 btnOK��	btnOK��$��55��  CFactoryReset_xaml_btnOK#���    ButtonStyle1#���  $��12(�$��Segoe UI)�$
��Bold"� � Cursor$
 Hand��  
ListBorder#���  $��2��$��Bottom=�$
��Left=�$��90��$ 91��$	 142���� -   $
 	btnCancel��	btnCancel��   CFactoryReset_xaml_btnCancel#���  #���  $��12(�$��Segoe UI)�$
��Bold"�$
 Hand��#���  $��3��$��55��$��Bottom=�$��Right=�$��90��$	 224��$	 142��� -	   $��	pbFactory��	pbFactory��$��15�� � Value$ 0Q�$y�Hidden=�$��Bottom=�$ 12��$	 214��$	��393��� -
   $ 
LayoutRoot��
LayoutRoot��
������ $��Center=�$��Center=�$��58��$��53��$	 341��$	 145��z��� SpinnerScale��-   SpinnerScale�� �� ScaleX$	 1.0Q� �� ScaleY$	 1.0Q�.+��� Canvas_1�� J� RenderTransformOrigin$ 0.5,0.5&�.+�A� 	Ellipse_1��$��10��$��10��$ 6��$ 2�� �� Stretch$
 Fill=�
��� ��� J� Opacity$	 1.0Q�A� 	Ellipse_2��$��10��$��10��$ 2��$ 16��$
 Fill=�
���   �$	 0.9Q�A� 	Ellipse_3��$��10��$��10��$ 5��$ 30��$
 Fill=�
���   �$	 0.8Q�A� 	Ellipse_4��$��10��$��10��$ 14��$ 38��$
 Fill=�
���   �$	 0.7Q�A� 	Ellipse_5��$��10��$��10��$ 27��$ 38��$
 Fill=�
���   �$	 0.6Q�A� 	Ellipse_6��$��10��$��10��$ 40��$ 32��$
 Fill=�
���   �$	 0.5Q�A� 	Ellipse_7��$��10��$��10��$ 43��$ 17��$
 Fill=�
���   �$	 0.4Q�A� 	Ellipse_8��$��10��$��10��$ 39��$ 4��$
 Fill=�
���   �$	 0.2Q�A� 	Ellipse_9��$��10��$��10��$ 18��$ -3��$
 Fill=�
���   �$	 0.1Q�A� 
Ellipse_10��$��10��$��10��$ 30��$ -2��$
 Fill=�
���   �$	 0.3Q�z��� SpinnerRotate��-   SpinnerRotate�� �� Angle$ 0Q� � Triggers :� EventTrigger_1�� :� RoutedEvent$ ContentControl.Loaded��.[��� BeginStoryboard_1��.���� Storyboard_1��.��W� -    W�Name$
 Spin��Spin�� �� 
TargetName$ SpinnerRotate�� �� TargetProperty$ (RotateTransform.Angle)� W� From$ 0X�	 W� To$	 360X� g� Duration$ 0:0:01D� g� RepeatBehavior$ Forever��