   M S B A M L   `   `   `  ����H  DWpfAppAdo.net, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WpfAppAdo.netWpfAppAdo.net  5   	   "    WpfAppAdo.net.PasswordWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:WpfAppAdo.net     5   	     � Title$  Авторизация��5   	    � SizeToContent$ WidthAndHeight=�6    � WindowStartupLocation$ CenterScreen=�6<   $	��341��6a   ���	#FFB0D2EF � 
ResizeMode$ NoResize=�6�    � Icon$ 
/___83.png��6�   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $��30 �6   �� 5      $��20 �6   �� 5      $��30 �6   �� 5      $��20 �6   �� 5      $��20 �6   �� 5      $��20 �6   �� 5      $
��Auto �6   .+�5   
   �� $��Center=�6   $
��Left=�6/   Введите логин:6Z   �� 5   
   -   5        $��loginBox��5      $��1��6"   $	��250��6/   .��6   
postgres6�   �� 5   
   $��2��6   $��Center=�6!   $
��Left=�6<   Введите пароль:6h   G� 5   
   -   5        $��passwordBox��5      $��3��6)   $	��250��66    G� Password$ Пароль��6E   6W   � 5   
   $��6��6   $��
191,15,0,0q�6!   .+�5      �� -   5         ��Name$ 	btnAccept��5       �� 	IsDefault$
 True��6(   $��60��69   $��0,0,10,0q�6G   .��6q   OK6u   �� 5      -   5        $ 	btnCancel��5      	 �� IsCancel$
	 True��6(   $��60��68   .��6F   Отмена6N   5      �� 5   
   -   5        
 ��Name$
 chBoxSaveData��5      $A��;Сохранить данные о пользователе��6*   $
��Left=�6T   $��10,0,0,0q�6o   $��5��6�   $��Center=�6�    J� RenderTransformOrigin$ 
0.5,-0.319&�6�    d� 	IsChecked$ FalseY�6�   5      5      