   M S B A M L   `   `   `  ����   BMC.Presentation.POS.    (BMC.Presentation.POS.Views.PasswordRetry   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     �Name$  Window�� � Title$ PasswordRetry��$
��Auto��$
��Auto�� � AllowsTransparency .  � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=���Z� .����    LoginInputBG#���    �� Orientation$ Vertical=�$	��350��$	��460��.+��  � RowDefinitions �� RowDefinition_1��$��15 ��� RowDefinition_2��$��25 �.+���   PasswordRetry_xaml_Title#���  $��Segoe UI)�$��20(�$
��Bold"�$��29��$��1��$��Bottom=�$��Center=�� -    �Name$ grdLoginFields��grdLoginFields��$	��450��$	��230�� �� Left$ 344.569��
	 �� Top$	 500.037�� �� RowDefinition_1��$��70 ��� RowDefinition_2��$��70 ��� RowDefinition_3��$��70 �
 � ColumnDefinitions
 �� ColumnDefinition_1��$��15 ��� ColumnDefinition_1��$	��200 ��� ColumnDefinition_2��$��216.404 �.+�G� -    G�Name$ txtOldPassword��$��2��$��0��  LoginPasswordBoxStyle#���  $��1��$��16(�$��Center=� G� 	MaxLength$ 50��G� -   $ txtNewPassword��$��2��$��1��#���  $��2��$��16(�$��Center=�$ 50��G� -   $ txtRetypePassword��$��2��$��2��#���  $��3��$��16(�$��Center=�$ 50���� -    ��Name$ lblOldPassword�� % !PasswordRetry_xaml_lblOldPassword#���  $��Segoe UI)�$��18(�$
��Bold"�$
��21.6��$��1��$��0��$��Center=��� -   $ lblNewPassword�� % !PasswordRetry_xaml_lblNewPassword#���  $��Segoe UI)�$��18(�$
��Bold"�$
��21.6��$��1��$��1��$��Center=��� -   $ lblConfirmPassword�� ) %PasswordRetry_xaml_lblConfirmPassword#���  $��1��$��Segoe UI)�$��18(�$
��Bold"�$
��21.6��$��2��$��Center=��� $ 
Horizontal=�$��Center=�$��Center=�.+��� -	   $��btnOK��  
BMC_Button#���  $��Bottom=�$��55��  MsgBox_xaml_btnOK#���  $��4���� $��40���� -
   $��	btnCancel��#���  $��Bottom=�$��55��  MsgBox_xaml_btnCancel#���  $��5���� $��40��