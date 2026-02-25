ALTER TABLE expeditions
ADD COLUMN idnotes INT,
ADD FOREIGN KEY (idnotes) REFERENCES notes_internes(id);