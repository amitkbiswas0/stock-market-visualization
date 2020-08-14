CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE stocks (
	id UUID DEFAULT uuid_generate_v4() NOT NULL,
	trade_code VARCHAR(50) NOT NULL,
	trade_date DATE NOT NULL,
	open VARCHAR(50),
	high VARCHAR(50),
	close VARCHAR(50),
	low VARCHAR(50),
	volume VARCHAR(50),
	PRIMARY KEY(id)
);

-- COPY stocks
-- (trade_date, trade_code, high, low, open, close, volume) 
-- FROM 'path_to_csv' delimiter ',' csv header;