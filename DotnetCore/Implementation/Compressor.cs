using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Implementation
{
    public class Compressor : FileHandler
    {
        //Debug
        //private int bytesWritten = 0;

        private int bitesToWrite = 0;

        protected Dictionary<byte, KeyValuePair<int,int>> mapping = new Dictionary<byte, KeyValuePair<int,int>>(); ///Rep/length

        protected long bytesCount;

        protected int[] counter = new int[1 << 8];

        protected int[] constantCounter = new int[1 << 8];

        protected byte[] values = new byte[1 << 8];

        protected int presentSymbols;

        public Compressor(Stream input) : base(input)
        {
            FillValues();
            bytesCount = input.Length;
        }

        private void FillValues()
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = (byte)i;
            }
        }

        public Compressor(string path) : base(path)
        {
            FillValues();
            bytesCount = memoryStream.Length;
        }


        protected void CountDataOpt()
        {
            //memoryStream.Position = 0;
            ResetStream(memoryStream);
            long fileLength = bytesCount;
            byte currentByte;
            for (int i = 0; i < fileLength; i++)
            {
                int entero = memoryStream.ReadByte();
                currentByte = (byte)entero;
                counter[currentByte]++;
                constantCounter[currentByte]++;
            }
            presentSymbols = (1 << 8) - Array.FindAll(counter, (a) => a == 0).Length;
        }

        protected void CountData()
        {
            byte currentByte;
            long fileLength = bytesCount;
            for (int i = 0; i < fileLength; i++)
            {
                currentByte = (byte)memoryStream.ReadByte();
                counter[currentByte]++;
                constantCounter[currentByte]++;
            }
            presentSymbols = (1 << 8) - Array.FindAll(counter, (a) => a == 0).Length;
        }

        protected void SortData()
        {
            Array.Sort(counter, values);
        }

        protected override void BuildNodes()
        {
            int margin = (1 << 8) - presentSymbols;
            for (int i = 0; i < presentSymbols; i++)
            {
                nodes.AddLast(new LeafNode(values[i + margin], counter[i + margin]));
            }
        }
     
        protected void SetSequence(GenericNode root, int sequence, int depth)
        {
            if (root is LeafNode)
            {
                var leaf = root as LeafNode;
                byte b = leaf.Value;
                mapping[b] = new KeyValuePair<int, int>(sequence, depth);
            }
            else
            {
                var nonLeaf = root as NonLeafNode;
                SetSequence(nonLeaf.Right, (sequence << 1) | 1, depth + 1);
                SetSequence(nonLeaf.Left, sequence << 1, depth + 1);
            }
        }

      

        public override void RunProccess(FileStream output)
        {
            //CountData();
            CountDataOpt();
            SortData();
            BuildNodes();
            BuildTree();
            SetSequence(nodes.First.Value, 0,0);
            Write(output);
#region DEBUG
            //Counting(_nodes.First.Value, 0);
            //long original = bytesCount * 8;
            //bytesToWrite += (mapp.Keys.Count * 8) + (mapp.Keys.Count * sizeof(Int16));

            //long converted = bytesToWrite;
            //var percent = 100 - ((double)converted * 100) / (double)original;
#endregion
        }
       
        private void Counting(GenericNode root, int depth)
        {
            if (root is LeafNode)
            {
                var leaf = root as LeafNode;
                bitesToWrite += depth * constantCounter[leaf.Value];
            }
            else
            {
                var nonLeaf = root as NonLeafNode;
                Counting(nonLeaf.Right, depth + 1);
                Counting(nonLeaf.Left, depth + 1);
            }
        }
   
        protected override void Read(FileStream file)
        {
            throw new NotImplementedException();
        }

        protected override void Write(FileStream newFile)
        {
            var memory = new MemoryStream();
            //var bw = new BinaryWriter(newFile);
            short charactAmount = (short)presentSymbols;
            int bla = charactAmount & 0xff00;
            bla >>= 8;
            byte[] amounts = new byte[2] { (byte)(charactAmount & 0x00ff), (byte)(bla) };
            memory.Write(amounts, 0, 2);
            //bw.Write(charactAmount);//8bits  ---- 16bits: 2 bytes
            //bytesWritten += 2;
            for (int i = (1 << 8)-charactAmount; i < (1 << 8); i++)
            {
                byte character = values[i];
                memory.WriteByte(character);
                int amount = counter[i];
                int _3 = (int)(amount & 0xff000000) >> 24;
                int _2 = (amount & 0x00ff0000) >> 16;
                int _1 = (amount & 0x0000ff00) >> 8;
                int _0 = (amount & 0x000000ff);
                byte[] arr = new byte[4] { (byte)_0, (byte)_1, (byte)_2, (byte)_3, };
                memory.Write(arr, 0, 4);
                //bw.Write(character);//8bits X each: 1 byte
                //bytesWritten += 1;
                //bw.Write(amount);//32bits X each  : 4 bytes 
                //bytesWritten += 4;
            }
           
            bitesToWrite =0;
            Counting(nodes.First.Value,0);
            var bitArray = new BitArray(bitesToWrite);
            //binaryReader.BaseStream.Position = 0;

            //binaryReader.BaseStream.Position = 0;
            //binaryReader.BaseStream.CopyTo(memory);
            //memoryStream.Position =0;
            ResetStream(memoryStream);
            for (int i = 0, arrayIndex=0; i < bytesCount; i++)
            {
                //byte read = binaryReader.ReadByte();
                //byte read = (byte)memory.ReadByte();
                byte read = (byte)memoryStream.ReadByte();
                var pair = mapping[read];
                var length = pair.Value;
                var sequence = pair.Key;
                for (int j = length-1; j >= 0; j--)
                {
                    bitArray.Set((j + arrayIndex), sequence % 2 == 1);
                    sequence >>= 1;
                }
                arrayIndex += length;
            }
            //Write to file the bit array
            byte toWrite = 0;
            int bites = 0;
            foreach (var item in bitArray)
            {
                bites++;
                var b = (bool)item;
                toWrite <<= 1;
                if (b)
                    toWrite |= 1;
                if (bites == 8)
                {
                    bites = 0;
                    memory.WriteByte(toWrite);
                    //bw.Write(toWrite);
                    //bytesWritten += 1;
                    toWrite = 0;
                }
            }
            if (bites != 0)
            {
                toWrite <<= (8 - bites);
                memory.WriteByte(toWrite);
                //bw.Write(toWrite);
                //bytesWritten += 1;
            }
            //memory.Position = 0;
            ResetStream(memory);
            memory.CopyTo(newFile);
            memory.Close();
            //bw.Close();
        }
    }
}
