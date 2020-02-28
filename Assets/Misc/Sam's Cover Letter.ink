->start
==start
Hey I'm sam! And this is the cover letter for the programmer postion at Drowning A Mermaid
*Nice to meet you, I'm also called Sam.
Wow, what a strange counidence.
*Hello, it just so happens that my boss is also called Sam. 
No way, I had no idea!
-
*it's a bit risky making a game your cover letter right?
A little bit. But then again, go big or go home right?
    **I agree
    I thought you might! it's part of why I really want to work with you all.
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
->inspritation
*Why you want to work here?
->why
{CHOICE_COUNT() == 0 } ->Conclusion



==Experience
Can do, so I graduated from undergrad last May, and since then I worked at Nickelodian on there interactive video team. Whilst there I made short form episodes of Blues a clues,
*Could you be more specific about your role?
Yes! My job was to take all the finalized assets (video files, animations, sounds) and assmeble them into a completed episode in unity. The team had alot of premade scripts to speed up the process. 
    **What did you learn?
    Outside of adjusting to work life generelly, I think this job really honed my skills of communicating between diffrent departments, most of the people in the team came from a television backround, so learning how to elegantly explain problems that arised withen this werid workflow was very important/
        ***Intresting
        I agree 
        ->Select

->Select



==inspritation

->Select
==why


->Conclusion
==Conclusion

Thank you so much for considering me. I hope that Drownin Gamer Maid goes well. 
*ummm, actually the name of this compnay is drowning a mermaid
what did I say?
**Drownin Gamer Maid.
Oh no...I'm so embrassed!
***don't worry about it. 
phew. ok I'm going to go now. Bye!
-->END




