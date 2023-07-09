namespace NetSession
{
public enum NERR
    {
        /// <summary>
        /// Operation was a success.
        /// </summary>
        NERR_Success = 0,
        /// <summary>
        /// More data available to read. dderror getting all data.
        /// </summary>
        ERROR_MORE_DATA = 234,
        /// <summary>
        /// Network browsers not available.
        /// </summary>
        ERROR_NO_BROWSER_SERVERS_FOUND = 6118,
        /// <summary>
        /// LEVEL specified is not valid for this call.
        /// </summary>
        ERROR_INVALID_LEVEL = 124,
        /// <summary>
        /// Security context does not have permission to make this call.
        /// </summary>
        ERROR_ACCESS_DENIED = 5,
        /// <summary>
        /// Parameter was incorrect.
        /// </summary>
        ERROR_INVALID_PARAMETER = 87,
        /// <summary>
        /// Out of memory.
        /// </summary>
        ERROR_NOT_ENOUGH_MEMORY = 8,
        /// <summary>
        /// Unable to contact resource. Connection timed out.
        /// </summary>
        ERROR_NETWORK_BUSY = 54,
        /// <summary>
        /// Network Path not found.
        /// </summary>
        ERROR_BAD_NETPATH = 53,
        /// <summary>
        /// No available network connection to make call.
        /// </summary>
        ERROR_NO_NETWORK = 1222,
        /// <summary>
        /// Pointer is not valid.
        /// </summary>
        ERROR_INVALID_HANDLE_STATE = 1609,
        /// <summary>
        /// Extended Error.
        /// </summary>
        ERROR_EXTENDED_ERROR= 1208,
        /// <summary>
        /// Base.
        /// </summary>
        NERR_BASE = 2100,
        /// <summary>
        /// Unknown Directory.
        /// </summary>
        NERR_UnknownDevDir = (NERR_BASE + 16),
        /// <summary>
        /// Duplicate Share already exists on server.
        /// </summary>
        NERR_DuplicateShare = (NERR_BASE + 18),
        /// <summary>
        /// Memory allocation was to small.
        /// </summary>
        NERR_BufTooSmall = (NERR_BASE + 23)
    }
}