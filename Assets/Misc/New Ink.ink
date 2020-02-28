->start
==start
Hey I'm sam! And this game is my cover letter for the programmer postion at Drowning  A Mermaid
*nice to meet you, I'm also called Sam.
Wow, what a strange counidence.
*Hey, it just so happens that my boss is also called Sam. 
No way, I had no idea!
-
*it's a bit risky making a game as your cover letter right?
A little bit. But then again, go big or go home right?
    **I agree
    I thought you might! It's part of why I really want to work with you all.
    ->Select
    **I don't agree
    uhhhh..well..ummm, would you like a more straight forward version? 
        ***yes please#normal
        ->END
        ***lets see where this goes.
        ok sweet.
        ->Select

==Select
{&what would you like to hear about first? |What would you like to hear about next?|What would you like to hear about next?| }
*Your previous experience?
->Experience
*Your Inspiration
->Inspritation
*Why you want to work here?
->why
{CHOICE_COUNT() == 0 } ->Conclusion



==Experience
Can do, so I graduated last May, and since then I've worked at Nickelodian on their interactive video team.
*Could you be more specific about your role?
Yes! My job was to take all the finalized assets (video files, animations, sounds) and assmeble them into a completed interactions for each episode, the idea being that 


The team had a lot of tools made to speed up the process, so it was very cool.
    **What did you learn?
    Outside of adjusting to work life generelly, I think this job really honed my skills of communicating between diffrent departments, most of the people in the team came from a television backround, so learning how to elegantly explain problems that arised within this werid workflow was very important.
        ***Intresting
        ->Select

->Select



==Inspritation

For me, I'm always inspired by things I've never seen before. 


->Select
==why

While I was at nickelodian It just 


->Conclusion
==Conclusion

Thank you so much for considering me. I hope that Drownin Gamer Maid goes well. 
*ummm, actually the name of this compnay is" Drowning A Mermaid"
what did I say?
**Drownin Gamer Maid.
Oh no...that's embarrassing.
***don't worry about it. 
phew. ok I'm going to go now. Bye!
-->END




