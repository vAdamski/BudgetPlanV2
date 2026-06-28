SELECT 'CREATE DATABASE budgetplan_identity'
WHERE NOT EXISTS (
	SELECT FROM pg_database WHERE datname = 'budgetplan_identity'
)\gexec
