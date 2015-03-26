using System.Collections.Generic;
using System.IO;

namespace CompressionCore
{
    public abstract class FileHandler
    {
        protected LinkedList<GenericNode> nodes = new LinkedList<GenericNode>();

        protected MemoryStream memoryStream;

        protected FileHandler(Stream input)
        {
            memoryStream = new MemoryStream();
            input.CopyTo(memoryStream);
        }

        protected FileHandler(string path)
        {
            memoryStream = new MemoryStream();
            var fileStream = new FileStream(path, FileMode.Open);
            fileStream.CopyTo(memoryStream);
        }

        protected virtual void BuildTree()
        {
            while (nodes.Count > 1)
            {
                GenericNode a = nodes.First.Value;
                GenericNode b = nodes.First.Next.Value;
                GenericNode c = new NonLeafNode(a, b);
                nodes.Remove(a);
                nodes.Remove(b);
                SetNodeInPosition(c);
            }
        }

        protected void SetNodeInPosition(GenericNode current)
        {
            var curr = nodes.First;
            for (int i = 0; i < nodes.Count; i++)
            {
                if (curr.Value.Weight > current.Weight)
                {
                    nodes.AddBefore(curr, current);
                    return;
                }
                curr = curr.Next;
            }
            nodes.AddLast(current);
        }

        protected abstract void BuildNodes();

        public abstract void RunProccess(FileStream output);

        protected abstract void Read(FileStream file);
     
        protected abstract void Write(FileStream newFile);

        protected void ResetStream(Stream stream)
        {
            stream.Position = 0;
        }
    }
}
