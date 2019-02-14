/*
 * Copyright (c) 2017 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy of
 * the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 */

// [START speech_quickstart]


using Google.Cloud.Speech.V1;
using System;
using System.IO;


namespace GoogleCloudSamples
{
    public class QuickStart
    {
        public static void Main(string[] args)
        {

            // string value = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
            // Console.WriteLine(value);
           
            

            var speech = SpeechClient.Create();
            var longOperation = speech.LongRunningRecognize(new RecognitionConfig()
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                SampleRateHertz = 8000,
                LanguageCode = "en",
            }, RecognitionAudio.FromFile("OSR_us_000_0010_8k.wav"));
            longOperation= longOperation.PollUntilCompleted();
            var response = longOperation.Result;

            using (System.IO.StreamWriter file =
           new System.IO.StreamWriter(@"C:\Users\Emmanuel\Documents\Engineering 4\Degree\Outputs\out_test.txt", true))
            {
                foreach (var result in response.Results)
                {
                    foreach (var alternative in result.Alternatives)
                    {
                        file.WriteLine(alternative.Transcript);
                        //Console.WriteLine(alternative.Transcript);
                        //System.IO.File.WriteAllText(@"C:\Users\Emmanuel\Documents\Engineering 4\Degree\Outputs\out_test.txt", alternative.Transcript);

                    }
                }
            }
        }
    }
}
// [END speech_quickstart]
