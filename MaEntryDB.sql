SELECT * FROM matakuliah
SELECT * FROM Jadwal
SELECT * FROM Users                --Query Untuk Melihat Table
SELECT * FROM mahasiswa
SELECT * FROM prodi

-- 1. Tabel Users (Login untuk semua user: mahasiswa & dosen)
CREATE TABLE Users (
    user_id SERIAL PRIMARY KEY,
    username VARCHAR(20) NOT NULL UNIQUE, -- NIM/NIP
    password VARCHAR(255) NOT NULL,       -- Enkripsi MD5 atau hash
    role VARCHAR(20) NOT NULL             -- 'mahasiswa' atau 'dosen'
);

-- 2. Tabel Program Studi
CREATE TABLE Prodi (
    prodi_id SERIAL PRIMARY KEY,
    nama_prodi VARCHAR(100) NOT NULL UNIQUE
);

-- 3. Tabel Mahasiswa
CREATE TABLE Mahasiswa (
    nim VARCHAR(12) PRIMARY KEY,
    nama VARCHAR(100) NOT NULL,
    user_id INTEGER NOT NULL,
    prodi_id INTEGER NOT NULL,
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (prodi_id) REFERENCES Prodi(prodi_id)
);

-- 4. Tabel Dosen
CREATE TABLE Dosen (
    nip VARCHAR(16) PRIMARY KEY,
    nama VARCHAR(100) NOT NULL,
    user_id INTEGER NOT NULL,
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);

-- 5. Tabel Mata Kuliah
CREATE TABLE MataKuliah (
    matakuliah_id SERIAL PRIMARY KEY,
    nama_matakuliah VARCHAR(100) NOT NULL,
    prodi_id INTEGER NOT NULL,
    FOREIGN KEY (prodi_id) REFERENCES Prodi(prodi_id)
);

-- 6. Tabel Form Absensi
CREATE TABLE Form_Absensi (
    id_absensi SERIAL PRIMARY KEY,
    nim VARCHAR(12) NOT NULL,
    nama_mahasiswa VARCHAR(100),
    nip VARCHAR(16) NOT NULL,
    nama_dosen VARCHAR(100),
    tanggal DATE NOT NULL,
    waktu TIME NOT NULL,
    status VARCHAR(10) NOT NULL, -- hadir, alpa, izin
    matakuliah_id INTEGER NOT NULL,
    FOREIGN KEY (nim) REFERENCES Mahasiswa(nim),
    FOREIGN KEY (nip) REFERENCES Dosen(nip),
    FOREIGN KEY (matakuliah_id) REFERENCES MataKuliah(matakuliah_id)
);

CREATE TABLE Jadwal (
    jadwal_id SERIAL PRIMARY KEY,
    matakuliah_id INTEGER NOT NULL,
    hari VARCHAR(10) NOT NULL,          -- Senin, Selasa, ...
    jam_mulai TIME NOT NULL,            -- misal: 08:00
    jam_selesai TIME NOT NULL,          -- misal: 09:40
    FOREIGN KEY (matakuliah_id) REFERENCES MataKuliah(matakuliah_id)
);


INSERT INTO Prodi (nama_prodi) VALUES
('Teknologi Informasi'), 
('Sistem Informasi'),
('Infromatika');

INSERT INTO MataKuliah (nama_matakuliah, prodi_id) VALUES
('Pemrograman Berorientasi Objek', 1),
('Basis Data', 1),
('Etika Profesi', 2);

-- TO ADD DATA JADWAL
INSERT INTO Jadwal (matakuliah_id, hari, jam_mulai, jam_selesai) VALUES
(2, 'Tuesday', '08:00', '09:40'),
(2, 'Thursday', '10:00', '11:40');
