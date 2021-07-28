CREATE TABLE IF NOT EXISTS genre (
      id SERIAL
    , name VARCHAR(64) UNIQUE NOT NULL
    , PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS director (
      id SERIAL
    , first_name VARCHAR(64) NOT NULL
    , middle_name VARCHAR(64)
    , last_name VARCHAR(64) NOT NULL
    , date_of_birth DATE NOT NULL
    , PRIMARY KEY (id)
    , UNIQUE (first_name, middle_name, last_name, date_of_birth)
);

CREATE TABLE IF NOT EXISTS person (
      id SERIAL
    , email VARCHAR(128) UNIQUE NOT NULL
    , username VARCHAR(128) UNIQUE NOT NULL
    , first_name VARCHAR(64) NOT NULL
    , last_name VARCHAR(64) NOT NULL
    , date_of_birth DATE
    , PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS movie (
      id SERIAL
    , title VARCHAR(128) NOT NULL
    , year INTEGER NOT NULL
    , duration INTEGER NOT NULL
    , director_id INTEGER REFERENCES director(id) 
    , rating INTEGER
    , PRIMARY KEY (id)
    , UNIQUE (title, year, director_id)
);

CREATE TABLE IF NOT EXISTS view (
      movie_id INTEGER REFERENCES movie(id)
    , person_id INTEGER REFERENCES person(id)
    , rating INTEGER
    , PRIMARY KEY (movie_id, person_id)
);

CREATE TABLE IF NOT EXISTS genred_movie (
      movie_id INTEGER REFERENCES movie(id)
    , genre_id INTEGER REFERENCES genre(id)
    , PRIMARY KEY (movie_id, genre_id)
);