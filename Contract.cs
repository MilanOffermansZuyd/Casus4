﻿namespace Casus4
{
    public class Contract
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Foto { get; set; }
        public bool IsSigned{ get; set; }
        public PhotoShoot? ForPhotoShoot{ get; set; }
        public Contract(int id, string name, byte[] foto, bool issigned, PhotoShoot? forPhotoShoot) 
        {
            Id = id;
            Name = name;
            Foto = foto;
            IsSigned = issigned;
            ForPhotoShoot = forPhotoShoot ?? null;
        }
        public void Add(Contract fotoShoot)
        {
            throw new NotImplementedException();
        }
        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
