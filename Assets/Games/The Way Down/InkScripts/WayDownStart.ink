INCLUDE Overworld
INCLUDE StartHome
INCLUDE Conclude
INCLUDE Test1
INCLUDE Encounter2
INCLUDE Trader1

->Start

VAR KneeCaps = 3
VAR Age = 3
VAR Mood = "Neutral"
VAR Strange = "Eyeballs"
VAR StrangeAmount = 0

===Start
~ KneeCaps = 4
~ Age = 3
~ Mood = "Neutral"
~ Strange = "Eyeballs"
~ StrangeAmount = 0
game start
*test trader
->Trader1
*test game
->StartHome
*fse
This is where the game starts #m #1
    **wow
        cool #m #0
        ***dd 
        ddht
        ***dd
    ~KneeCaps = 4
    **amazing. {KneeCaps > 3: X}
    **cool
    -
    ->OverWorld