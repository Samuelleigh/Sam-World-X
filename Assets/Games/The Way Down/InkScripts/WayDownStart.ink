INCLUDE Utility/Overworld
INCLUDE Start/StartHome

INCLUDE World1/Stare
INCLUDE World1/RockThrow
INCLUDE World1/Laughing
INCLUDE World1/TurtleFollower
INCLUDE World1/Turtle
INCLUDE World1/PotionsPerson
INCLUDE World1/Builder
INCLUDE World1/TheLaw
INCLUDE Endings/OutofContent

INCLUDE Conclude
INCLUDE Test1
INCLUDE Encounter2
INCLUDE World1/Trader1
INCLUDE Utility/Function
INCLUDE Utility/Death
INCLUDE Utility/Potions
INCLUDE Utility/scapegoat


VAR KneeCaps = 4
VAR Age = 3
VAR Body = 5000
VAR Awareness = 10
VAR Mood = "Neutral"
VAR Strange = "Eyeballs"
VAR StrangeAmount = 0

VAR PotionSlot1 = 0
VAR PotionSlot2 = 0
VAR PotionSlot3 = 0
VAR PotionSlot4 = 0
VAR PotionSlot5 = 0
VAR PotionSlot6 = 0

~ KneeCaps = 4
~ Age = 3
~ Body = 5000
~ Awareness = 10
~ Mood = "Neutral"
~ Strange = "Eyeballs"
~ StrangeAmount = 0
->Turtle


===Start
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
    **coold
    -
    ->OverWorld 