   M S B A M L   `   `   `  ����   BMC.CoreLib(    "BMC.CoreLib.WPF.AxWpfAsyncProgress   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � 	Resources     CoreProgBar1 �� 
TargetType ��Name �� EndPoint � Offset � Color  
TitleBrush �� 
StartPoint��%-  &	        &	 �     �� " �.R�� .+��� $ 
PART_Track������ $	 1,0&�.��� $ 0Q�$ DarkBlue��� $ 1Q�$ 	LightBlue���� $ PART_Indicator��$
��Left=�
��� ���
���   �$��1���� $ 0,0.5&�$ 1,0.5&�.��� $ 0Q�$ 	DarkGreen��� $ 1Q�$ ForestGreen��.���� $��0q�
������ ���#193441$��1,1,1,1q� �� CornerRadius$ 4,4,4,4�� �� Padding$ 5q�._�� 	 � RowDefinitions	 �� $��30 ��� $��* �.+��� $��0��$��2q�#��[ 
���   �$��1q�._��� -   $��	lblStatus��$��2q�$
��Left=�$��Center=�$��Calibri)�$��14(�$
��Bold"�
�������� -   $��
LayoutRoot��$��1��$��Stretch=�$��Stretch=�	 �� $��* �
 � ColumnDefinitions
 �� $��* ��� $	��120 �.+��� -   $��	btnCancel��$��1��$��2,5,2,2q�$��Stretch=�$��Stretch=�$��Cancel��� -   $��	prgStatus��$��0��$��0��$��1��$��2,5,2,2q�$��Stretch=�$��Stretch=� � IsIndeterminate .  