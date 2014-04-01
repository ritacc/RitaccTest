CREATE TABLE SYS_LOG
(
	LOG_ID				bigint			NOT NULL	IDENTITY(1,1)
	,EVENT_ID			int				NOT NULL		-- 事件ID, 事件可能是错误/警告/提示
	,EVENT_TIME			datetime		NOT NULL		-- 事件时间
	,[PRIORITY]			int				NULL			-- 优先级
	,SEVERITY			int				NULL			-- 紧急
	,MACHINE_NAME		nvarchar(512)	NOT NULL		-- 设备名称
	,SERVER_NAME		nvarchar(512)	NOT NULL		-- 服务器名称
	,PROCESS_ID			int				NULL			-- 进程ID
	,PROCESS_NAME		nvarchar(512)	NOT NULL		-- 进程名称
	,THREAD_ID			int				NULL			-- 线程ID
	,THREAD_NAME		nvarchar(512)	NOT NULL		-- 线程名称|方法名称|数据库过程名称
	,THREAD_LINE		int				NULL			-- 行号
	,[MESSAGE]			nvarchar(max)	NOT NULL
	,FOMATTED_MESSAGE	nvarchar(max)	NOT NULL
)