# Say
A simple, command-line based utility for having your Windows PC say something to you (text-to-speech)

### Why?
A valid question. If you had a SoundBlaster in the 90's and ever played around with Dr. Sbaitso or maybe even a TI-994a in the 80's with the Speech module,
you know speech synthesis can be fun to fool around with. It's hilarious to make your computer say silly things... and kids love it!

But, believe it or not, it can be useful, too! I've found that it's a fantastic notification tool, verbally informing you that a long process has ended.
Think software builds. Big file copies. Downloads. It _really_ impresses people when you're sitting at your desk "not doing much", waiting on a long compile,
when suddenly everyone nearby hears "Project _whatever_ has finished building." Welcome to the future. A future where you look cool and your own computer
has legitimized the feet you've propped up on your desk even as your manager stands by.

### The Details
Say is a C# console app for Windows that I originally developed under VS2008 and now fooled around with in VS2019. The package manager should download the
appropriate version of the Speech SDK automatically.

If you're running something older than Windows 7, then you're on your own... but it should run on anything newer.

As for running Say... it works in 3 ways:

- As a one-shot: run it with the things you want to say in quotes, as the command-line parameter.
```
cmd> Say "Hello human, welcome to the future. Kind of a future from the past. A forgotten technology. Yeah. Hello."
```

- As an interactive console: run it with no parameters and then you can type line after line of text for it to say.

- As an SSML processor: run it with the redirection operation ("<<") to provide a file with [Speech Synthesis Markup Language](https://en.wikipedia.org/wiki/Speech_Synthesis_Markup_Language) content.

For the first two use cases, there is a way that you can specify the gender for a block of text; add "/m" for male, "/f" for female, and "/n" for neutral...
So, under the first use case, you could do something like:
```
cmd> Say /m "Where would you like to have dinner?" /f "I don't know, where would you like to have dinner?" /m "I don't know either. Maybe I will eat a neighborhood cat."
```

In the second use case, let the gender command be on a line by itself.

### As a tool
In Visual Studio, you can add a Post-Build step command... this is where you might use Say.
```
Say $(ProjectName) has finished building.
```

### Is that all?
Pretty much. Have fun and Say responsibly; computers eventually develop suggestive speech patterns, filled with inuendo, in my experience... use at your own risk.

### License
"I don't care." Give me credit if you use Say and feel charitable. Make a pull request if you improve it and I might put it in, who knows. Use it for profit (?)
or loss of profit (!) but I'm not responsible for any loss of, or damage to, anything - including, but not limited to, your reputation with co-workers, friends,
spouses, children, pets, sentient robots, et al - as a result of the use of Say.
