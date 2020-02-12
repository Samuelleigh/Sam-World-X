VAR numberOfQuestion = 0
VAR last = 0


//====Phase 1 Start: Discovering that the rock is not going to respond====
->Start
==Start==
...
*[Hey.]
*[Hi]
*[What's up?]
-
...
*[So I guess you're the rock I'm going to have a conversation with.]
    ...
    **[What's your name?]
    ->Questions
    **[Are you ok?]
    ->Questions
    **[So are you a real rock or...]
    ->Questions
    **[This is dumb, I'm going to leave.]
    ->Leaving
*[Hi?]
  ->HiLoop
*[Don't be rude, speak!]
...
    **[HELLLOOO! Aren't you going to talk back?]
    **[Is there something wrong?]
    --
    ->Questions
    **[Look I don't have all day, if your not going to say anything then I'm going to leave.]
    ->Leaving
    
==HiLoop
...
    + [{&Hello.|Hey.|How's life.|How is your day going.|Hi Hi.|High Five.|Hey.}]
    ...
    ->HiLoop
    *[Is everything ok?]
    ->Questions
    
==Questions==
...
    *[Can you speak? It looks like you have a mouth?]
    ~ numberOfQuestion = numberOfQuestion + 1
    ->Questions
    *[Can you see me? It looks like you have eyes?]
     ~ numberOfQuestion = numberOfQuestion + 1
     ->Questions
    *[Can you hear me? I can't see any ears but...]
     ~ numberOfQuestion = numberOfQuestion + 1
     ->Questions
     * {numberOfQuestion > 1}[Ok, your a rock. Just a rock.]
     ->phase2


==Leaving
...
    +[{&Leaving right now. |I'm just about to do something else.|Talking to a rock is a waste of my time, so I'm leaving. |Going now.| Whatever I'm done. | For real this time, I'm going.}]
    ->Leaving
    *[{&Actually I'll stay.|Ok you've got what's your deal.|This is dumb but I guess I'll go along with it after all.|I have nothing better to do.|I have nothing I'd rather be doing.|Now that I've reconsidered it, yes I do want to talk to this rock.| What's up rock?}]
    ->Questions

//============Phase 2 Start: Not actually talking to the rock============
==phase2
...
*[So...now what?]
...
    **[I could tell you about myself.]
    ...
        ***[If that's ok with you.]
        ...
            ****[Cool!]
            -> IntroduceMyself
        ***[Ok I guess I'll just start?]
            ->IntroduceMyself
    **[I guess I could try talking to you about whatever.]
    ->akwardConvo
    
*[This is stupid.]
->ThisSucks


==IntroduceMyself //talk abut myself
...
*[My name is {~Karl|Frank|Sarah|Estella|Keerthi|Desi|Yuval|Ayo}.]
->IntroduceMyself
* [I'm actually quite famous belive it or not.]
...
    **[Yes I actually {~invented the Oscilloscope.|wrote the lyrics to "I Will Always Love You".|discovered the Titanic's wreckage. |.co-created Myspace.}]
        ...
        *** [But most recently I {~swam across the atlantic.|produced 4 episodes of a comedic web series for PBS.|found the cure for most cancers.|ate the world's last rainbow tongued swordfish.}]
        ...
        *** [But before that I was most known for {~eating 150 hotdogs in a row.|producing 4 episodes of a comedic web series for PBS.|found the cure for most cancers.|ate the world's last rainbow tongued fish.}]
        ...
        ***[I may not have done anything before or since then, but still.]
        ---
        ->prettyCoolhuh
    **[Not that you would care or anything.]
    ...
        ***[Because you're a rock.]
        ->akwardConvo
*[I actually used to be a rock.]
...
    **[100 million years ago, all the atoms in my body were part of rocks.]
    ...
        ***[So we actually have a lot in common.]
            ->prettyCoolhuh
        ***[You must get that all the time.]
            ->akwardConvo

==akwardConvo //conversation topic that is really boring ()
...
*[Ehhh, how's your day been going so far?]
...
    **[My day has been alright.]
    ...
    **[My day kinda sucked.]
    ...
    **[My day has been Great.]
    ...
    --
        **[I tried oat milk for the first time today.]
        ...
            ***[It was really nice.] 
            ...
                ****[Which I was surprised by.]
                ...
                    *****[Because I tried almond milk years ago and hated it.]
                    ->IntroduceMyself_TransOut
                    *****[Because fads are normally a waste of time.]
                     ->IntroduceMyself_TransOut
                ****[Not that I was surprised.]
                    *****[I love oats...so.]
                     ->IntroduceMyself_TransOut
                    *****[I love milk...so.]
                     ->IntroduceMyself_TransOut
            ***[I didn't really like it.]
          
            ------
            ->IntroduceMyself_TransOut
        **[I saw a dead rat on the sidewalk.]
        ...
            ***[It was gross.]
            ...
            ***[It was sad.]
            ...
            ***[It was sad and gross.]
            ...
            ---
            ***[I stared at it for an uncomfortable amount of time.]
            ...
            ***[I looked away immediately.]
            ...
            ---
            ***[I hope it's gone now.]
            ...
                ****[I don't like being reminded of death.]
                ->IntroduceMyself_TransOut
                ****[It's unsanitary.] 
                ->IntroduceMyself_TransOut
            ***[I guess there isn't much difference between talking to a dead rat and a rock.]
            ...
                ****[Except I guess a dead rat used to be alive.]
                ->IntroduceMyself_TransOut
                ****[Functionally at least.]
                ->IntroduceMyself_TransOut
            
==prettyCoolhuh
...
*[Pretty cool huh.]
->IntroduceMyself_TransOut

==IntroduceMyself_TransOut
...

    **[Ummm.]
    ...
        ***[This conversation sucks.]
        ->ThisSucks
        ***[You're such a great listener.]
        ->GreatListener
        
        
==GreatListener
...
*[Do you want to hear a secret?]
->secret
*[I bet you would listen to anything.]
->Anythingd

==Anythingd
...
*[oufh poawuhfo]
...
    **[aeef]
    ...
    **[11322e4]
    ...
    **[dsv]
    ...
    --
    **[orange.]
    **[lcd]
    **[gummy]
   
    --
    ->NowWhat
*[dohadohaoh]
...
    **[aeef]
    ...
    **[11322e4]
    ...
    **[dsv]
    ...
    --
    **[orange]
    **[lcd]
    **[gummy]
    --
    ->NowWhat
*[fpokpokpokpkp]
...
    **[aeef]
   ...
    **[11322e4]
    ...
    **[dsv]
    ...
    --
    **[orange]
    **[lcd]
    **[gummy]
     --
    ->NowWhat


==secret
...
*[It's a pretty dark secret.]
...
    **[But I think I can trust you.]
    ...//here a popup window will appear where you can write a dark secret
        ***[Submit Dark Secret.]
        ...
            ****[You're not judging me right.]
                *****[No of course not...you're a rock.]
                ->IntroduceMyself_TransOut
            ****[Wow that was cathartic.Thanks For that.]
            ...
                ->NowWhat
            ****[Wow, I feel nothing.]
                ->ThisSucks
            
        ***[Actually I've changed my mind.]
        ->DenySecret
    **[Actually I don't want to tell you.]
    ->DenySecret
    
    
===DenySecret
    ...
        ***[Because you have no sentience, I would just be talking to myself.] //
        ...
        ->WhyDoesItSuck
            
        ***[I have no idea who might be listening in.] //segway to meta conversation
        ...
        ->NowWhat
        ***[But it's nice not having to explain myself.]
        ->NowWhat
        *[Could you tell me your secrets?]
->RockTellsYouASecret



==RockTellsYouASecret
...
*[Go on.]
...
*[Don't be shy.]
...
-
*[Umm sorry can you speak up.]
...
*[Please say something.]
...
-
**[Literally Anything.]
...
**[Go on. I belive in you.]
...
-
*[This sucks.]
->ThisSucks
*->WhyDoesItSuck
...
==ThisSucks
...
*[I'm going to try to think about why this sucks.]
->WhyDoesItSuck
*[This sucks in a way that so obvious it's below further thought.]
...
    **[I'm going to leave.]
    ->leavePhase2
    
==WhyDoesItSuck
...
*[It's impossible to talk to a rock.]
...
    **[Conversation requires a back and fourth.]
    ...
        ***[So I'm really just having a conversation with myself.]
        ->unless
        ***[And I have no way of doing that.]
        ->unless
    **[A rock is not alive, so it cannot exert it's will back onto me.]
    ->unless
*[It's pointless to talk to a rock.]
...
    **[Because it isn't conscious, I can learn nothing from it.]
    ...
        ***[Ok, maybe I could gain some information from it.]
        ...
            ****[Like maybe I could analyze it's composition to find some info.]
            ...
            ****[Or I could think.] 
            ...
            ----
            ****[But I don't care about that.]
            ...
            ****[But for what ends.]
            ...
            ----
            ->NowWhat
        ***[Yep nothing.]
        ->END
    **[I could be doing something else and get more value out of it.]
    ...
        ***[Like learning a trade.]
        ...
        ***[Like eating a really nice sandwich.]
        ...
        ***[Like texting a friend.]
        ...
        ---
        ***[This is just a waste of time in comparison.]
        ->NowWhat
        
*{IntroduceMyself > 1 || akwardConvo > 1}[I think I'm just approaching this the wrong way]
->Refelection

==Refelection
...
*[I could try listening?]
->tryListening
    
==unless
...
    *[Unless I throw it, like a dice, and make a decision on how it lands.]
        ...
        **[Though I guess that could be said of any object.]
           ...
            ***[You hear that rock! There's nothing special about you.]
            ->NowWhat
            ***[I hear nothing]
            ->NowWhat
        **[It's more a conversation with physics than anything else.]
        ...
            ***[Which could be cool.]
            ***[Which would be equally meaningless.]
            ---
            ->NowWhat
    *[Unless I change my definition of conversation.]
    ...
        **[Maybe a conversation is just any sort of feedback loop]
        ...
            ***[I mean, the rock's silence has impacted my thoughts...right.]
            ->NowWhat
    *[Unless the rock actually just start talking.]
        ...
        **[Which I know it could..]
        ...
             ***[..Because it's not really a rock]
             ...
                 ****[..Your just a picture of a rock]
                 ...
                    *****[...and if the author of this game wanted to.]
                    ...
                        ******[He could make you speak.]
                        ...
                            *******[Also..]
                            ...
                                ********[They're not exactly giving me many options on how to converse with you anyway.]
                                ->NowWhat

==leavePhase2
->END

==RockCouldTalk
...
*[Because you're not really a rock.]
...
    **[You're just a picture of a rock.]
    ->NowWhat

//Entering Phase 3

==NowWhat
...
*[I don't really know how to continue]
...
-
*[I guess I could try listening to you.]
->tryListening
*[I'm going to leave.]
->END

//===============================Phase 3 Proper===========================

==tryListening
...
+[...]
 ~ last = last + 1
->tryListening
+ {last > 4 }[I'm ready to move on to something else.]
->END

