import os
os.system("pause")
from pydub import AudioSegment
from pydub.silence import split_on_silence
os.system("pause")

def match_target_amplitude(aChunk, target_dBFS):
    ''' Normalize given audio chunk '''
    change_in_dBFS = target_dBFS - aChunk.dBFS
    return aChunk.apply_gain(change_in_dBFS)

song = AudioSegment.from_wav("OSR_us_000_0010_8k.wav")

#split track where silence is 2 seconds or more and get chunks

chunks = split_on_silence(song, 
    # must be silent for at least 2 seconds or 2000 ms
    min_silence_len=800,

    # consider it silent if quieter than -16 dBFS
    #Adjust this per requirement
    silence_thresh=-30 
)

#Process each chunk per requirements
for i, chunk in enumerate(chunks):
    #Create 0.5 seconds silence chunk
    silence_chunk = AudioSegment.silent(duration=500)

    #Add  0.5 sec silence to beginning and end of audio chunk
    audio_chunk = silence_chunk + chunk + silence_chunk

    #Normalize each audio chunk
   # normalized_chunk = match_target_amplitude(audio_chunk, -20.0)

    #Export audio chunk with new bitrate
    print("exporting chunk{0}.wav".format(i) )
    audio_chunk.export(".//chunk{0}.wav".format(i), bitrate='192k', format="wav")
	
os.system("pause")
raw_input()