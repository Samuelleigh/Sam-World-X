INCLUDE Normal
INCLUDE Package
INCLUDE Help
INCLUDE Remember
INCLUDE Final

VAR reaction = ""

*Annoyed
->normal.annoyed
*Neutral
->normal.neutral
*Expecting
->normal.expectingfriend
*final
->needsfriend.start


=testing

+ Set Reaction to annoyed
~ reaction = "annoyed"
->testing
+Set Reaction to Neutral
~ reaction = "neutral"
->testing
+ Set Reaction to expecting
~ reaction = "Expecting"
->testing

*package
    **AnnoyedPackage
->package.annoyedPackage
->package.neutralPackage
->package.expectingPackage
->package.WherePackage
->package.why
->package.signHere
->package.WhereTruck
->package.annoyedinjury


{ reaction: 
- "annoyed":
- "neutral": 
- "Expecting":
}