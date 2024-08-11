<p align="center">
  <a href="https://postimg.cc/"><img src="https://i.postimg.cc/3NCTY9rx/screencapturelogo.png"></a>
</p>
<h1 align="center">Simple Screen Recorder</h1>

<h2 align="center">Introduction</h2>
<p align="left">
Simple Screen Recorder is a tool that allows users to record their screen activity and audio. Here are instructions on how to use the Simple Screen Recorder to create screen recordings and audio recordings.
</p>

<h2 align="center">How to use it?</h2>
<p align="center">

- In the main screen recording window, select the **audio input device** that you want to use to record the audio track from the **Microphone (Mic/Aux)** dropdown menu.<br>

- Select the **audio output device** that you want to use to record the audio track from the **System sound (Desktop Audio)** dropdown menu.<br>

- Select the **codec** that you want to use from the **Encoder** dropdown menu. The available codecs are "MPEG-4", "H264 NVENC (Nvidia Graphics Cards)" and "H264 AMF (AMD Graphics Cards)".<br>

- Select the **file format** that you want to use from the **File Format** dropdown menu. The available formats are "MKV", "AVI", "WMV". **I recommend to use MKV**.<br>

- Select the **framerate** that you want to use from the **Video framerate** dropdown menu. You can record a video at 30 or 60 fps.<br>

- Select the **monitor** that you want to record from the **Monitor selector** dropdown menu.<br>

- If you check the **Capture all monitors** option, the app is going to record a video with all the monitors connected to the PC.<br>

- In the section **Audio recording method**, use the combobox to select the audio component you want to record, such as desktop audio or a connected microphone.<br>

- Click the **Start Recording** button to begin the screen recording process. The timer in the lower left corner of the window will	start counting up.<br>

- To stop the screen recording, click the **Stop Recording** button. The recorded video file will be saved in the "Recordings" folder.<br>

- When you enter the "Recordings" folder, you'll see the audio and video files separated. Having files like this is very useful if you want to edit the video and audio tracks with total freedom. However, you also can combine/merge your audio and video files using the "Media Merge" tool. All the merged files will be saved in the "Output Files" folder.<br>

- You can also record audio separately. You can record your microphone, desktop audio, or both separately. 
Select the Audio Recording option in the main window and you can record audio from your PC. The recorded audio files are stored in the "Audio Recordings" folder.<br>
</p>

<h2 align="center">Important installation and use notice for Simple-Screen-Recorder</h2>
To ensure that Simple-Screen-Recorder functions correctly without any issues related to video recording, please adhere to the following guidelines when choosing a location:

**Avoid using the application in the Program Files**  directory or any other system-protected areas that might require elevated permissions.

**Avoid lengthy file paths.** For example, avoid paths like C:\Users\lextrack\source\repos\lextrack\Simple-Screen-Recorder\bin\Release\net8.0-windows10.0.22621.0\publish\win-x64\Recordings as they can lead to issues with file management and recording functionality.

**Do not rename the application's folder.** Renaming could lead to issues with accessing the necessary files for recording.

To prevent **Windows UAC prompts from interrupting recording**, you should select the second-to-last option, which says "Notify me only when apps try to make changes to my computer (do not dim my desktop)". Or, if you want, completely disable it.

Where can you **install the app**?

- Your "Documents" folder.

- Or extract it in a simple root directory, such as "C:\Simple-Screen-Recorder".

<h2 align="center">Similar project</h2>
<p align="left">
I have made another <a href="https://github.com/lextrack/MiniScreenRecorder">screen recorder</a> for Windows and Linux. It is even simpler and lighter, and the resulting video recording is in MP4 or MKV format with included audio without the need for conversion. Additionally, it allows recording a specific area of the screen.
</p>

<h2 align="center">Acknowledgments</h2>

<p>Thanks to <strong>Flaticon</strong> for the icons, <strong>FFmpeg</strong> for their incredible framework, <strong>NAudio</strong>, and <strong>TutorialesVbNET</strong> for giving me the inspiration to create this.</p>

