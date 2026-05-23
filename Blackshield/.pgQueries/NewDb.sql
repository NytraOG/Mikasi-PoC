CREATE USER blackshield WITH PASSWORD 'dev-only-do-not-use-in-prod';
CREATE DATABASE blackshield OWNER blackshield;
GRANT ALL PRIVILEGES ON DATABASE blackshield TO blackshield;