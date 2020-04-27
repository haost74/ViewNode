using NAudio.Wave;
using System;
using System.IO;

namespace Audio
{
    public class MainAudio
    {
        // WaveIn - поток для записи
        private WaveIn waveIn;
        //Класс для записи в файл
        private WaveFileWriter writer;
        //Имя файла для записи
        public string outputFilename = "имя_файла.wav";

        public MainAudio()
        {
            if (File.Exists(outputFilename)) File.Delete(outputFilename);
        }

        //Получение данных из входного буфера 
        async void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            //Записываем данные из буфера в файл
            //writer.WriteData(e.Buffer, 0, e.BytesRecorded);
            //writer.WriteData(e.Buffer, 0, e.BytesRecorded);
            await writer.WriteAsync(e.Buffer, 0, e.BytesRecorded);

        }

        //Начинаем запись - обработчик нажатия кнопки
        public void StartRec()
        {
            try
            {
                waveIn = new WaveIn();
                //Дефолтное устройство для записи (если оно имеется)
                //встроенный микрофон ноутбука имеет номер 0
                waveIn.DeviceNumber = 0;
                //Прикрепляем к событию DataAvailable обработчик, возникающий при наличии записываемых данных
                waveIn.DataAvailable += waveIn_DataAvailable;
                //Прикрепляем обработчик завершения записи
                waveIn.RecordingStopped += WaveIn_RecordingStopped;
                //Формат wav-файла - принимает параметры - частоту дискретизации и количество каналов(здесь mono)
                waveIn.WaveFormat = new WaveFormat(8000, 1);
                //Инициализируем объект WaveFileWriter
                writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
                //Начало записи
                waveIn.StartRecording();
            }
            catch (Exception ex)
            {
                using(StreamWriter sw = new StreamWriter("errors.txt", true))
                {
                    sw.WriteLine(DateTime.Now + " | " + ex.ToString() + "\r\n");
                }
            }
        }
        //Окончание записи
        private void WaveIn_RecordingStopped(object sender, StoppedEventArgs e)
        {
            waveIn.Dispose();
            waveIn = null;
            writer.Close();
            writer = null;
        }


        //Завершаем запись
        void StopRecording()
        {
            waveIn.StopRecording();
        }

        //Прерываем запись - обработчик нажатия второй кнопки
        public void StopRec()
        {
            if (waveIn != null)
            {
                StopRecording();
            }
        }

    }
}
