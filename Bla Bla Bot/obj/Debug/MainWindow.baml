   M S B A M L   `   `   `  ����F  BBla Bla Bot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Bla_Bla_BotBla_Bla_Bot  5   	       Bla_Bla_Bot.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Bla_Bla_Bot     5   	     � Title$  Bla Bla Bot��5   	   $	��450��6   $	��800��6*   .��5	      � .+�5
   
   �� $
��Left=�6   $��42��6/   $��	0,377,0,0q�6;   $	��Top=�6N   $	��782��6f   .@�5      �� -   5         ��Name$ status_label��5      $*��$Состояние: Выключен��6*   �� 5      �� 5      -   5        $ eventscount_label��5      $��Событий: 0��6/   �� 5      -   5        $ messagescount_label��5      $��Сообщений: 0��61   5      �� 5   
   $��Состояние��6   $
��Left=�6&   $��86��6A   $��	10,10,0,0q�6M   $	��Top=�6`   $	��122��6x   .��5      � .+�5      �� -   5         ��Name$ enable��5      $��Включить��6)   $
��Left=�6<   $��	10,10,0,0q�6W   $	��Top=�6j   $��93��6�   6�   �� 5      -   5        $ disable��5      $��Выключить��6*   $
��Left=�6>   $��	10,35,0,0q�6Y   $	��Top=�6l   $��93��6�   {�.  6�   5      5      �� 5   
   -   5         ��Name$ list_guilds��5      $
��Left=�6'   $	��250��6B   $��
10,122,0,0q�6O   $	��Top=�6c   $	��200��6{   6�   �� 5   
   $#��Список серверов��6   $
��Left=�6*   $��	10,96,0,0q�6E   $	��Top=�6X   �� 5   
   -   5         ��Name$ log_box��5      $
��Left=�6#   $	��362��6>   $��
215,10,0,0q�6K    �� TextWrapping$
 Wrap=�6_   $	��Top=�6s   $	��567��6�   $ �� HorizontalScrollBarVisibility$ Visible=�6�   " �� VerticalScrollBarVisibility$
 Auto=�6�    �� 
IsReadOnly . 6�   5      5      