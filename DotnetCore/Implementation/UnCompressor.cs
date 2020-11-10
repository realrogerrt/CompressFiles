using Implementation.Util;
using System;
using System.IO;

namespace Implementation
{
    public class UnCompressor : FileHandler
    {
        private int fileLenght = 0;

        public UnCompressor(Stream input) : base(input)
        {
        }

        public UnCompressor(string path) : base(path)
        {
        }

        protected override void BuildNodes()
        {
            byte []buffer = new byte[2];
            //memoryStream.Position = 0;
            ResetStream(memoryStream);
            memoryStream.Read(buffer, 0, 2);
            short _0 = buffer[0];
            short _1 = buffer[1];
            _1 <<= 8;
            short symbolsAmount = (short)(_0 | _1);
            for (int i = 0; i < symbolsAmount; i++)
            {
                byte value = (byte)memoryStream.ReadByte();
                buffer = new byte[4];
                memoryStream.Read(buffer, 0, 4);
                int b0 = buffer[0]; 
                int b1 = buffer[1]; b1 <<= 8;
                int b2 = buffer[2]; b2 <<= 16;
                int b3 = buffer[3]; b3 <<= 24;
                int valueFrequence = b0; valueFrequence |= b1; valueFrequence |= b2; valueFrequence |= b3;
                fileLenght += valueFrequence;
                var leaf = new LeafNode(value, valueFrequence);
                nodes.AddLast(leaf);
                }
        }

        public override void RunProccess(FileStream output)
        {
            BuildNodes();
            BuildTree();
            Write(output);
        }

        protected override void Read(FileStream file)
        {
            throw new NotImplementedException();
        }

        protected override void Write(FileStream newFile)
        {
            var memory = new MemoryStream();
            int writeCounter = 0;
            var currentRoot = nodes.First.Value;
            //while (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length)
            while (memoryStream.Position != memoryStream.Length)
            {
                byte branch = (byte)memoryStream.ReadByte();
                var iterator = new BitIterator(branch);
                foreach (var item in iterator)
                {
                    var nonLeaf = currentRoot as NonLeafNode;
                    if (item)
                    {
                        currentRoot = nonLeaf.Right;
                    }
                    else
                    {
                        currentRoot = nonLeaf.Left;
                    }
                    if (currentRoot is LeafNode)
                    {
                        var leaf = currentRoot as LeafNode;
                        var value = leaf.Value;
                        memory.WriteByte(value);
                        //bw.Write(value);
                        writeCounter++;
                        if (writeCounter == fileLenght)
                        {
                            goto CloseStream;
                        }
                        currentRoot = nodes.First.Value;
                    }
                }
            }
        CloseStream:
        ResetStream(memory);
        memory.CopyTo(newFile);
        }
    }
}
