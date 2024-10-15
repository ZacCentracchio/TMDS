

# Install the gTTS library
# pip install gtts

from gtts import gTTS
from pathlib import Path

# Text you want to convert to speech
text = "Today is a wonderful day to build something people love!"

# Convert text to speech
tts = gTTS(text)

# Save the speech to a file
tts.save("speech.mp3")

# Play the saved speech (Optional, depending on your environment)
import os
os.system("start speech.mp3")  # For Windows, for Linux/Mac use "open" or "xdg-open"
