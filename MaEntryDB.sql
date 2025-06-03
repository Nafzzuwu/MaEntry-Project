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

--MENAMBAHKAN KOLOM NIP PADA TABEL MATAKULIAH
ALTER TABLE MataKuliah ADD COLUMN nip character varying(16);
ALTER TABLE MataKuliah ADD CONSTRAINT fk_matakuliah_dosen 
FOREIGN KEY (nip) REFERENCES Dosen(nip);

--TAMBAH DATA MATAKULIAH
INSERT INTO MataKuliah (nama_matakuliah, prodi_id) VALUES
('Pemrograman Berorientasi Obyek', 1),
('Pemrograman Berorientasi Obyek (PRAKTIKUM)', 1),
('Bahasa Indonesia', 1),
('Matematika Diskrit', 1),
('Teori Graf', 1),
('Algoritma dan Pemrograman II', 1),
('Sistem Basis Data', 1),
('Sistem Basis Data (PRAKTIKUM)', 1),
('Jaringan Komputer', 1),
('Jaringan Komputer (PRAKTIKUM)', 1),
('Sistem Operasi', 1),
('Sistem Operasi (PRAKTIKUM)', 1);

INSERT INTO MataKuliah (nama_matakuliah, prodi_id) VALUES
('Bahasa Indonesia', 2),
('Sistem Basis Data', 2),
('Sistem Basis Data (PRAKTIKUM)', 2),
('Jaringan Komputer', 2),
('Jaringan Komputer (PRAKTIKUM)', 2),
('Sistem Operasi', 2),
('Sistem Operasi (PRAKTIKUM)', 2);

INSERT INTO MataKuliah (nama_matakuliah, prodi_id) VALUES
('Bahasa Indonesia', 3),
('Sistem Basis Data', 3),
('Sistem Basis Data (PRAKTIKUM)', 3),
('Jaringan Komputer', 3),
('Jaringan Komputer (PRAKTIKUM)', 3),
('Sistem Operasi', 3),
('Sistem Operasi (PRAKTIKUM)', 3);

--TAMBAH DATA JADWAL
-- SENIN
INSERT INTO Jadwal (matakuliah_id, hari, jam_mulai, jam_selesai, prodi_id)
SELECT matakuliah_id, 'Monday', '08:00', '09:40', prodi_id
FROM MataKuliah
WHERE nama_matakuliah IN (
    'Pemrograman Berorientasi Obyek',
    'Bahasa Indonesia'
);

-- SELASA
INSERT INTO Jadwal (matakuliah_id, hari, jam_mulai, jam_selesai, prodi_id)
SELECT matakuliah_id, 'Tuesday', '10:00', '11:40', prodi_id
FROM MataKuliah
WHERE nama_matakuliah IN (
    'Matematika Diskrit',
    'Teori Graf'
);

-- RABU
INSERT INTO Jadwal (matakuliah_id, hari, jam_mulai, jam_selesai, prodi_id)
SELECT matakuliah_id, 'Wednesday', '13:00', '14:40', prodi_id
FROM MataKuliah
WHERE nama_matakuliah IN (
    'Algoritma dan Pemrograman II',
    'Sistem Basis Data',
    'Jaringan Komputer',
    'Sistem Operasi'
);

-- KAMIS
INSERT INTO Jadwal (matakuliah_id, hari, jam_mulai, jam_selesai, prodi_id)
SELECT matakuliah_id, 'Thursday', '08:00', '09:40', prodi_id
FROM MataKuliah
WHERE nama_matakuliah IN (
    'Pemrograman Berorientasi Obyek (PRAKTIKUM)',
    'Sistem Basis Data (PRAKTIKUM)'
);

-- JUMAT
INSERT INTO Jadwal (matakuliah_id, hari, jam_mulai, jam_selesai, prodi_id)
SELECT matakuliah_id, 'Friday', '08:00', '09:40', prodi_id
FROM MataKuliah
WHERE nama_matakuliah IN (
    'Jaringan Komputer (PRAKTIKUM)',
    'Sistem Operasi (PRAKTIKUM)'
);

--MENGSET TABEL FORM_ABSENSI NIP AGAR BISA NULL
ALTER TABLE Form_Absensi
ADD CONSTRAINT unique_absen_per_hari
UNIQUE (nim, matakuliah_id, tanggal);

ALTER TABLE Form_Absensi
ALTER COLUMN nip DROP NOT NULL;

ALTER TABLE Form_Absensi
ADD CONSTRAINT form_absensi_nip_fkey
FOREIGN KEY (nip) REFERENCES Dosen(nip);
