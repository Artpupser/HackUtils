CREATE TABLE roles
(
    role_id SERIAL PRIMARY KEY,
    name    VARCHAR(64)
);
CREATE TABLE levels
(
    level_id     INTEGER PRIMARY KEY,
    name         VARCHAR(64),
    required_exp INTEGER
);
CREATE TABLE users
(
    user_id       SERIAL PRIMARY KEY,
    role_id       INTEGER,
    level_id      INTEGER,
    email         VARCHAR(254),
    password_hash VARCHAR(255),
    full_name     VARCHAR(64),
    experience    INTEGER,
    FOREIGN KEY (role_id) REFERENCES roles (role_id),
    FOREIGN KEY (level_id) REFERENCES levels (level_id)
);
CREATE TABLE sessions
(
    id         SERIAL PRIMARY KEY,
    user_id    INTEGER,
    token      VARCHAR(255),
    created_at TIMESTAMP,
    expires_at TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES users (user_id)
);
CREATE TABLE images
(
    image_id SERIAL PRIMARY KEY,
    url      TEXT
);
CREATE TABLE tours
(
    tour_id     SERIAL PRIMARY KEY,
    title       VARCHAR(150),
    description TEXT,
    price       NUMERIC(10, 2),
    image_id    INTEGER,
    FOREIGN KEY (image_id) REFERENCES images (image_id)
);
CREATE TABLE user_tours
(
    user_tour_id SERIAL PRIMARY KEY,
    user_id      INTEGER,
    tour_id      INTEGER,
    guide_option VARCHAR(16),
    status       VARCHAR(16),
    FOREIGN KEY (user_id) REFERENCES users (user_id),
    FOREIGN KEY (tour_id) REFERENCES tours (tour_id)
);
CREATE TABLE payments
(
    payment_id         SERIAL PRIMARY KEY,
    user_tour_id       INTEGER,
    amount             NUMERIC(10, 2),
    status             VARCHAR(16),
    yukassa_payment_id VARCHAR(150),
    created_at         TIMESTAMP,
    FOREIGN KEY (user_tour_id) REFERENCES user_tours (user_tour_id)
);
CREATE TABLE mushrooms
(
    mushroom_id SERIAL PRIMARY KEY,
    name        VARCHAR(150),
    description TEXT,
    location    VARCHAR(255),
    image_id    INTEGER,
    FOREIGN KEY (image_id) REFERENCES images (image_id)
);
CREATE TABLE mushroom_submissions
(
    submission_id SERIAL PRIMARY KEY,
    user_id       INTEGER,
    mushroom_id   INTEGER,
    image_id      INTEGER,
    status        VARCHAR(16),
    FOREIGN KEY (user_id) REFERENCES users (user_id),
    FOREIGN KEY (mushroom_id) REFERENCES mushrooms (mushroom_id),
    FOREIGN KEY (image_id) REFERENCES images (image_id)
);
CREATE TABLE bonuses
(
    bonus_id         SERIAL PRIMARY KEY,
    title            VARCHAR(150),
    description      TEXT,
    discount_percent SMALLINT,
    qr_code          VARCHAR(255)
);
CREATE TABLE user_bonuses
(
    user_bonus_id SERIAL PRIMARY KEY,
    user_id       INTEGER,
    bonus_id      INTEGER,
    status        VARCHAR(16),
    FOREIGN KEY (user_id) REFERENCES users (user_id),
    FOREIGN KEY (bonus_id) REFERENCES bonuses (bonus_id)
);
CREATE TABLE qr_codes
(
    qr_id        SERIAL PRIMARY KEY,
    type         VARCHAR(32),
    reference_id INTEGER,
    content      TEXT
);
CREATE
OR REPLACE FUNCTION delete_expired_sessions() RETURNS TRIGGER AS $$
BEGIN
DELETE
FROM sessions
WHERE expires_at < NOW();
RETURN NULL;
END;
$$
LANGUAGE plpgsql;
CREATE TRIGGER trigger_delete_expired_sessions
    AFTER
        INSERT
        OR
UPDATE
    ON sessions FOR EACH STATEMENT EXECUTE FUNCTION delete_expired_sessions();
