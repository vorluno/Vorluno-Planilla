import React from 'react';

const EmptyState = ({
    icon,
    title,
    description,
    action
}) => {
    return (
        <div className="text-center py-12 px-4">
            {icon && (
                <div className="flex justify-center mb-4">
                    {icon}
                </div>
            )}
            {title && (
                <h3 className="text-lg font-medium text-gray-900 mb-2">
                    {title}
                </h3>
            )}
            {description && (
                <p className="text-gray-500 mb-6 max-w-md mx-auto">
                    {description}
                </p>
            )}
            {action && (
                <div className="flex justify-center">
                    {action}
                </div>
            )}
        </div>
    );
};

export default EmptyState;
