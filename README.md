# SynthRiders Custom Stages and Experiences

Unity Project template that help make Custom Stages and Experiences for [Synth Riders](https://synthridersvr.com/)

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

Currenlty supported on Unity 2019.4.21f1. It is recomended the installation of [Unity Hub](https://store.unity.com/download?ref=personal/) for the ease of management of multiple versions of Unity

If you plan on making exporting for Quest 1 Quest 2, make sure your installation of Unity 2019.4.21f1 has "Android Build Support", "Android SDK & NDK Tools", and "OpenJDK" modules installed! you can check/install it using Unity Hub by going to "Installs>Unity 2019.4.21f1>Settings Gear>Add modules"

### Installing

Just clone the contents of this repository and open the project on Unity

## Deployment

Open the Demo Scene and make the changes to the Assets, once all the changes are made be sure to apply the changes to the StageTile prefabs. To export just go to the menu SynthRiders > Export Stage Bundle, you will be asked to set the Name of the Stage Bundle, once the name is set press Export.

When the export is completed just copy the file with the .stage/.spinstage/.stagequest/.spinstagequest extension and paste it into the CustomStages folder inside Synth Riders game, and the stage should appear in the stage selection menu.



All elements inside the _CustomStageElements folder will be exported, you can use all the non-code Assets (such as Models, Textures, Prefabs, Audio clips, etc), to build your custom stage.

Please preserve the size of the Thumb image elements, for the correct display in the game.

Be aware that the names of the following items must not be changed:

    * _CustomStageElements/MenuItems/Thumb
    * _CustomStageElements/Skybox/CustomSkybox
    * _CustomStageElements/Prefabs/Platform
    * _CustomStageElements/Prefabs/StageTile01
    * _CustomStageElements/Prefabs/StageTile02
    * _CustomStageElements/Prefabs/StageTile03
    * _CustomStageElements/Prefabs/SpinEmitter
    * Experience/Timeline
    * Experience/Timeline/Main


If you don't want to replace the default platform or default spin emitter, just delete or move the prefabs out of the _CustomStageElements folder

If you would like to make an Experience, Move the "Experience" folder into _CustomStageElements, then move the "Timeline" prefab from the "Experience" folder into your Demo Scene hierachy

If you want to link a Custom Experience to a Custom Beatmap, Change the "Experience Tag" in your Stage and Beatmap to match. Both the Experience and Beatmap **MUST** have the same Experience tag or else the game will not link them. Experience Tags must contain "EXPERIENCE-" and then followed by any 4 combination of Letters and Numbers (For Example, EXPERIENCE-1A2B)




## FAQ

* **"My Stage doesn't show up in game!"**

While in game in the Song Select Menu, Look up and click the "Reload" icon and press "Find New". The game will look for your stage and add it to the list

* **"My experience won't show up in the Stage Select menu!"**

Experiences will **not** show up in the Stage Select menu if they have an Experience Tag. to access the experience, locate the beatmap it is attatched to, and click "Play". The game will prompt you if you would like to play the Experience for the selected song or not.

* **"I Clicked play on my Beatmap but it didn't ask me if I wanted to play an Experience!"**

Double Check that both your Experience File and your Beatmap have the same experience tag (See above). If they have matching experience tags, see First FAQ

* **"My stage doesn't save scores, and has no walls while playing!"**

The game is still treating your stage as an experience. Move/Delete the "Timeline" prefab and the "Main" timeline object from within _CustomStageElements


## Versioning

We use [SemVer](http://semver.org/) for versioning.


## Authors

* **Jhean Ceballos** - *Developer* - [shogoki-vnz](https://github.com/shogoki-vnz)


## Special Thanks

* **Rrye** - *Provided Custom Strobe shaders*
* **AKARaiden** - *Provided Custom Strobe system*
* **XrayXout** - *Bug Fixes and Improvements* - [XrayXout](https://github.com/Xrayxout)