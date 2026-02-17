SELECT
    r.session_id            AS BlockedSessionID,
    r.blocking_session_id   AS BlockingSessionID,
    s.login_name            AS BlockedLogin,
    s.status                AS BlockedStatus,
    r.wait_type,
    r.wait_time,
    r.wait_resource,
    st.text                 AS BlockedQuery
FROM sys.dm_exec_requests r
JOIN sys.dm_exec_sessions s
    ON r.session_id = s.session_id
CROSS APPLY sys.dm_exec_sql_text(r.sql_handle) st
WHERE r.blocking_session_id <> 0;
